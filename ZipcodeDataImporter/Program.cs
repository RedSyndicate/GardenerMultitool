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
using MongoDB.Driver;
using PlantDataImporter.Extensions;
using System.Text;
using PlantDataImporter;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Helpers;

namespace ZipcodeDataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            
            //var zipCodes = new Loader().Run(args[0], directory);
            //Console.WriteLine("Wow you made it this far? That's nuts.");
            //Console.WriteLine(zipCodes.ToString());
            //Console.ReadLine();
        }
    }

    public class Loader
    {
        private static MapperConfiguration Config => new(cfg =>
            cfg.CreateMap<ZipcodeHardinessDto, ZipcodeHardiness>()
                .ConstructUsing(ConstructHardinessZips));

        private static ZipcodeHardiness ConstructHardinessZips(
            ZipcodeHardinessDto dto,
            ResolutionContext context) =>
            new(
                new HardinessZone(int.Parse(dto.Zone.Remove(1))), 
                new Zipcode(dto.Zipcode.Length < 5 
                    ? new StringBuilder("0")
                        .Append(dto.Zipcode)
                        .ToString() 
                    : dto.Zipcode), 
                dto.TemperatureRange, 
                dto.ZoneTitle);            

        public IEnumerable<ZipcodeHardiness> Run(string path, string directory)
        {
            var mapper = Config.CreateMapper();
            
            var csvFolder = Path.Combine(directory, path);
            var files = Directory.GetFiles(csvFolder);

            //make records list
            var plants = new List<ZipcodeHardiness>();

            foreach (var file in files)
            {
                using var reader = new StreamReader(Path.Combine(csvFolder, file));
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<ZipcodeHardinessDto>();
                
                plants.AddRange(records.Select(mapper.Map<ZipcodeHardiness>));
            }

            return plants;
        }
    }
}
