using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace DataImporter
{
    public class Program
    {

        public static async Task<int> Main(params string[] args)
        {
            var command = new RootCommand("Used to import CSVs into our Domain");
            var plantImportOption = new Option<FileInfo>("--plant-csvpath", description: "Path to the folder that contains the Plant Import CSVs");
            plantImportOption.AddAlias("-p");

            var zipcodeHardinessImportOption = new Option<FileInfo>("--zipcodehardiness-csvpath", "Path to the folder that contains the Zipcode Hardiness CSVs");
            zipcodeHardinessImportOption.AddAlias("-zh");

            command.AddOption(plantImportOption);
            command.AddOption(zipcodeHardinessImportOption);

            command.SetHandler<FileInfo, FileInfo>(Convert, plantImportOption, zipcodeHardinessImportOption);

            return await command.InvokeAsync(args);
        }

        public static void Convert(FileInfo plantCSVFolderPath, FileInfo zipcodeHardinessCSVFolderPath)
        {
            PlantImporter.Run(plantCSVFolderPath.FullName);
            ZipcodeHardinessImporter.Run(zipcodeHardinessCSVFolderPath.FullName);
        }
    }
}
