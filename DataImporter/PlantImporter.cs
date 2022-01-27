using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using CsvHelper;
using DataImporter.Extensions;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.HumanUses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DataImporter
{
    public class PlantImporter
    {
        public static void Run(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var plants = (new PlantLoader().Run(folderPath, directory)).ToList();

            InitializeMappings();

            foreach (var plant in plants)
            {
                plant.Id = Guid.NewGuid();
            }

            var collection = new MongoClient("mongodb://localhost")
                .GetDatabase("gardeners-multitool")
                .GetCollection<Plant>(nameof(Plant)
                    .ToLowerInvariant());

            collection
            .InsertManyAsync(plants);

            var plantsfound = collection.Find(p => true).ToList();

            File.WriteAllText(Path.Combine(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"), "plant.json"), JsonConvert.SerializeObject(plantsfound));
            Console.ReadLine();
        }

        private static HashSet<string> PlantPropertyCSVParser(IEnumerable<Plant> plants)
        {
            var plantPropertySB = plants.Aggregate(new StringBuilder(), FlatString);
            var plantPropertiesHashSet = plantPropertySB.ToString().Split("\r\n").ToHashSet();
            var plantPropertyList = new List<string>();

            foreach (var property in plantPropertiesHashSet)
            {
                if (!plantPropertyList.Contains(property))
                {
                    plantPropertyList.Add(property);
                }
                else
                {
                    continue;
                }
            }
            Console.WriteLine(plantPropertyList.ToString());
            Console.ReadLine();

            return plantPropertiesHashSet;
        }

        static StringBuilder FlatString(StringBuilder sb, Plant plant)
        {
            //for soil pH:  ?? = if the value is null, create new object with -1, -1, else use value as-is
            //var soilPH = plant.SoilPH ?? new pH(-1, -1);
            //for plant.Height: if value is null/empty, write "0", else write the attribute.
            //var plantHeight = string.IsNullOrEmpty(plant.Height) ? "0" : plant.Height;

            var soilMoisture = plant.Name;

            //for List<IEnumerable> Types
            //attributes.ForEach(plantAttribute => sb.AppendLine(plantAttribute.Label));
            //return sb;
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^

            return sb.Append(soilMoisture.ToString());
        }

        private static void InitializeMappings()
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), new GuidGenerator());
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var types = typeof(IPlantAttribute).Assembly.GetTypes().Where(t => t.IsClass && t.IsAssignableTo(typeof(IPlantAttribute)));

            foreach (var t in types)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(t));
            }

            BsonClassMap.RegisterClassMap<pH>(map =>
            {
                map.AutoMap();
                map.MapCreator(ph => new pH(ph.MinimumpH, ph.MaximumpH));
            });

            BsonClassMap.RegisterClassMap<Plant>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(p => p.Id)
                    .SetIdGenerator(new GuidGenerator());
            });

            BsonClassMap.RegisterClassMap<Location>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(l => l.Id)
                    .SetIdGenerator(new GuidGenerator());
            });
        }
    }

    public class PlantLoader
    {
        private static MapperConfiguration Config => new(cfg =>
            cfg.CreateMap<PlantDto, Plant>()
                .ForMember(dest => dest.PlantType, opt => opt.MapFrom(src =>
                    src.PlantType
                        .ToLowerInvariant()
                        .ToPlantType()))
                .ForMember(dest => dest.SoilPH, opt => opt.MapFrom(src =>
                    src.SoilPH
                        .Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .TopH()))
                .ForMember(dest => dest.EcologicalFunction, opt => opt.MapFrom(src =>
                    src.EcologicalFunction
                        .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(new HashSet<IEcologicalFunction>(), AggregateEcologicalFunctions)))
                .ForMember(dest => dest.HumanUse, opt => opt.MapFrom(src =>
                    src.HumanUseCrop
                        .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Where(FilterBullshit)
                        .Aggregate(new HashSet<IHumanUse>(), AggregateHumanUses)))
                .ForMember(dest => dest.HardinessZone, opt => opt.MapFrom(src =>
                    src.HardinessZone
                        .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s == "?" ? 0.ToString() : s)
                        .ToArray()
                        .ToHardinessZoneRange()))
        );

        public IEnumerable<Plant> Run(string path, string directory)
        {
            var mapper = Config.CreateMapper();

            var csvFolder = Path.Combine(directory, path);
            var files = Directory.GetFiles(csvFolder);

            //make records list
            var plants = new List<Plant>();

            foreach (var file in files)
            {
                using var reader = new StreamReader(Path.Combine(csvFolder, file));
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<PlantDto>();
                plants.AddRange(records.Select(mapper.Map<Plant>));
            }

            return plants;
        }

        private static readonly List<string> _nonoWords = new() { "wax", "resin", "or polish", "resin or polish", "spray" };
        private static bool FilterBullshit(string str) => !_nonoWords.Contains(str.ToLowerInvariant());


        private static HashSet<IEcologicalFunction> AggregateEcologicalFunctions(HashSet<IEcologicalFunction> accumulator, string function)
        {
            accumulator.Add(EcologicalFunctions.Create(function));
            return accumulator;
        }

        private static HashSet<IHumanUse> AggregateHumanUses(HashSet<IHumanUse> accumulator, string humanUse)
        {
            accumulator.Add(HumanUses.Create(humanUse));
            return accumulator;
        }
    }
}
