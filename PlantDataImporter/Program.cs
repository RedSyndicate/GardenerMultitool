using CsvHelper;
using GardenersMultitool.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.HumanUses;
using PlantDataImporter.Extensions;
using System.Text;

namespace PlantDataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var plants = new Loader().Run(args[0], directory);

            PlantPropertyCSVParser(plants);

        }

        private static void PlantPropertyCSVParser(IEnumerable<Plant> plants)
        {
            //This method returns to the console a list of all uniques values of a
            //given label on the CSV files. See var attribute of FlatString() below.
            var plantPropertySB = plants.Aggregate(new StringBuilder(), FlatString);
            string[] plantPropertiesSBArray = plantPropertySB.ToString().Split("\r\n");
            List<string> propertyList = new List<string>();

            foreach (string property in plantPropertiesSBArray)
                if (propertyList.Contains(property))
                    continue;
                else
                    propertyList.Add(property);

            foreach (var item in propertyList)
                Console.WriteLine(item);

            Console.ReadLine();
        }

        static StringBuilder FlatString(StringBuilder sb, Plant plant)
        {
            var attribute = plant.EcologicalFunction;
            //if (attribute == null)
            //    return sb;
            //return sb.AppendLine(attribute);
            return sb.AppendLine(attribute); // doesn't compile for Lists.
        }
    }
    public class Loader
    {
        private static MapperConfiguration Config => new(cfg =>
            cfg.CreateMap<PlantDto, Plant>()
                .ForMember(dest => dest.PlantType, opt => opt.MapFrom(src =>
                    src.PlantType.ToLowerInvariant().ToPlantType()))
                .ForMember(dest => dest.SoilPH, opt => opt.MapFrom(src =>
                    src.SoilPH.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).TopH()))
                .ForMember(dest => dest.EcologicalFunction, opt => opt.MapFrom(src =>
                    src.EcologicalFunction.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(Clean)
                        .Aggregate(new HashSet<IEcologicalFunction>(), AggregateEcologicalFunctions)))
                .ForMember(dest => dest.HumanUse, opt => opt.MapFrom(src =>
                    src.HumanUseCrop.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Where(FilterBullshit)
                        .Select(Clean)
                        .Aggregate(new HashSet<IHumanUse>(), AggregateHumanUses)))
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

        private static string Clean(string arg1) => arg1;

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

