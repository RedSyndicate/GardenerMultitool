using CsvHelper;
using GardenersMultitool.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.HumanUses;
using GardenersMultitool.Domain.ValueObjects.HumanUses.Biomass;
using PlantDataImporter.Extensions;

namespace PlantDataImporter
{
    class Program
    {
        private static readonly MapperConfiguration _config;

        static Program()
        {
            _config = Config;
        }
        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;

            var mapper = _config.CreateMapper();

            //make list of file names (Annuals, aquatics, etc) iterate through list[]
            //var csvFolder = @"D:\ZainGit\RedSyndicateStuff\PermacultureData\CSV";
            //var jsonFolder = @"D:\ZainGit\RedSyndicateStuff\PermacultureData\JSON";
            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

            var csvFolder = Path.Combine(directory, args[0]);
            var jsonFolder = Path.Combine(directory, args[1]);

            var csvExists = Directory.Exists(csvFolder);
            var jsonExists = Directory.Exists(csvFolder);
            var files = Directory.GetFiles(csvFolder);

            //make records list
            var plants = new List<Plant>();

            //json serialize the records list (collection)
            foreach (var file in files)
            {
                using var reader = new StreamReader(Path.Combine(csvFolder, file));
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<PlantDto>();
                plants.AddRange(records.Select(mapper.Map<Plant>));
            }

            Console.ReadLine();
        }

        private static MapperConfiguration Config => new(cfg =>
            cfg.CreateMap<PlantDto, Plant>()
                .ForMember(dest => dest.SoilPH, opt => opt.MapFrom(src =>
                    src.SoilPH.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).topH()))
                .ForMember(dest => dest.EcologicalFunction, opt => opt.MapFrom(src =>
                    src.EcologicalFunction.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(cleanfunction)
                        .Aggregate(new HashSet<IEcologicalFunction>(), AggregateEcologicalFunctions)))
                .ForMember(dest => dest.HumanUse, opt => opt.UseDestinationValue()));

        private static string cleanfunction(string arg1) => arg1;

        private static HashSet<IEcologicalFunction> AggregateEcologicalFunctions(HashSet<IEcologicalFunction> accumulator, string function)
        {
            accumulator.Add(EcologicalFunctions.Create(function));
            return accumulator;
        }

        private static List<IHumanUse> AggregateHumanUses(List<IHumanUse> accumulator, string humanUse)
        {
            accumulator.Add(HumanUses.Create(humanUse));
            return accumulator;
        }
    }
}

