﻿using CsvHelper;
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
            if (args.Length < 1)
                return;

            var mapper = _config.CreateMapper();

            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

            var csvFolder = Path.Combine(directory, args[0]);
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

            Console.ReadLine();
        }

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
                            .Select(Clean)
                            .Aggregate(new HashSet<IHumanUse>(), AggregateHumanUses))
                )
            );

        private static readonly List<string> _nonoWords = new() { "wax", "resin", "or polish", "resin or polish", "spray" };

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

