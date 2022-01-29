using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace DataImporter
{
    public class Program
    {
        public static IConfigurationRoot Configuration;
        public static RootCommand Command;

        public static async Task<int> Main(params string[] args)
        {
            InitializeMappings();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            ConfigureCommandLineOptions();

            return await serviceProvider.GetService<Program>()?.Run(args)!;
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

            BsonClassMap.RegisterClassMap<ZipcodeHardinessZone>(map =>
            {
                map.AutoMap();
            });
        }

        private async Task<int> Run(string[] args)
        {
            return await Command.InvokeAsync(args);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddSingleton<IConfigurationRoot>(Configuration);
            serviceCollection.AddTransient<Program>();
        }

        private static void ConfigureCommandLineOptions()
        {
            Command = new RootCommand("Used to import CSVs into our Domain");
            var plantImportOption = new Option<FileInfo>("--plant-csvpath", "Path to the folder that contains the Plant Import CSVs");
            plantImportOption.AddAlias("-p");

            var zipcodeHardinessImportOption = new Option<FileInfo>("--zipcodehardiness-csvpath", "Path to the folder that contains the Zipcode Hardiness CSVs");
            zipcodeHardinessImportOption.AddAlias("-zh");

            Command.AddOption(plantImportOption);
            Command.AddOption(zipcodeHardinessImportOption);

            Command.SetHandler<FileInfo, FileInfo>(Convert, plantImportOption, zipcodeHardinessImportOption);
        }

        public static async Task Convert(FileInfo plantCsvFolderPath, FileInfo zipcodeHardinessCsvFolderPath)
        {
            if (plantCsvFolderPath != null)
                await PlantImporter.Run(Configuration.GetSection("DatabaseSettings:ConnectionString").Value, Configuration.GetSection("DatabaseSettings:Database").Value, plantCsvFolderPath.Name);

            if (zipcodeHardinessCsvFolderPath != null)
                await ZipcodeHardinessImporter.Run(Configuration.GetSection("DatabaseSettings:ConnectionString").Value, Configuration.GetSection("DatabaseSettings:Database").Value, zipcodeHardinessCsvFolderPath.Name);
        }
    }
}
