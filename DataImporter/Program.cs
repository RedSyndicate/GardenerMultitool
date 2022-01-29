using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataImporter
{
    public class Program
    {
        public static IConfigurationRoot Configuration;
        public static RootCommand Command;

        public static async Task<int> Main(params string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            ConfigureCommandLineOptions();

            return await serviceProvider.GetService<Program>()?.Run(args)!;
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
