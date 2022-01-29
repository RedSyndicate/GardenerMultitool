using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Extensions;
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
        public static async Task Run(string mongoUrl, string database, string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            var directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var zipCodesHardiness = (new ZipcodeHardinessLoader().Run(folderPath, directory)).ToList();

            InitializeMappings();

            await new MongoClient(mongoUrl)
                .GetDatabase(database)
                .GetCollection<ZipcodeHardinessZone>(nameof(ZipcodeHardinessZone)
                    .ToLowerInvariant())
                .InsertManyAsync(zipCodesHardiness);
        }

        private static void InitializeMappings()
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), new GuidGenerator());
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            BsonClassMap.RegisterClassMap<ZipcodeHardinessZone>(map =>
            {
                map.AutoMap();
            });

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
            {
                cfg.CreateMap<ZipcodeHardinessZoneDto, ZipcodeHardinessZone>()
                    .ForMember(
                        dest => dest.Zipcode,
                        opt => opt.MapFrom(
                            src => src.Zipcode.ToZipcode()))
                    .ForMember(
                        dest => dest.HardinessZone,
                        opt => opt.MapFrom(
                            src => src.Zone.ToHardinessZone()))
                    .ForMember(
                        dest => dest.TemperatureRange,
                        opt => opt.MapFrom(
                            src => src.TemperatureRange))
                    .ForMember(
                        dest => dest.ZoneTitle,
                        opt => opt.MapFrom(
                            src => src.ZoneTitle));
            });

            public IEnumerable<ZipcodeHardinessZone> Run(string path, string directory)
            {
                var mapper = Config.CreateMapper();

                var csvFolder = Path.Combine(directory, path);
                var files = Directory.GetFiles(csvFolder);

                //make records list
                var zipcodeHardinessList = new List<ZipcodeHardinessZone>();

                foreach (var file in files)
                {
                    using var reader = new StreamReader(Path.Combine(csvFolder, file));
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                    var records = csv.GetRecords<ZipcodeHardinessZoneDto>();

                    zipcodeHardinessList.AddRange(records.Select(mapper.Map<ZipcodeHardinessZone>));
                }

                return zipcodeHardinessList;
            }
        }

    }
}