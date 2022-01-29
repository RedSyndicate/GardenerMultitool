using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using CsvHelper;
using GardenersMultitool.Domain.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DataImporter
{
    public class ZipcodeHardinessImporter
    {
        public static void Run(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var zipCodesHardiness = (new ZipcodeHardinessLoader().Run(folderPath, directory)).ToList();

            InitializeMappings();

            new MongoClient("mongodb://localhost")
                .GetDatabase("gardeners-multitool")
                .GetCollection<ZipcodeHardiness>(nameof(ZipcodeHardiness)
                    .ToLowerInvariant())
                .InsertManyAsync(zipCodesHardiness);
        }

        private static void InitializeMappings()
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), new GuidGenerator());
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            BsonClassMap.RegisterClassMap<ZipcodeHardiness>();
            BsonClassMap.RegisterClassMap<HardinessZone>(map =>
            {
                map.AutoMap();
                map.MapCreator(hz => new HardinessZone(hz.Zone));

            });
            BsonClassMap.RegisterClassMap<HardinessZoneRange>(map =>
            {
                map.AutoMap();
                map.MapCreator(hzr => new HardinessZoneRange(hzr.MaximumHardinessZone, hzr.MinimumHardinessZone));
            });
        }
        public class ZipcodeHardinessLoader
        {
            private static MapperConfiguration Config => new(cfg =>
                cfg.CreateMap<ZipcodeHardinessDto, ZipcodeHardiness>()
                    .ForMember(
                        dest => dest.HardinessZone,
                        opt => opt.MapFrom(
                            src => src.Zone))
                    .ForMember(
                        dest => dest.Zipcode,
                        opt => opt.MapFrom(
                            src => src.Zipcode))
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
}