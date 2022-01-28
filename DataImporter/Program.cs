using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace DataImporter
{
    public class Program
    {

        public static async Task<int> Main(params string[] args)
        {
            var command = new RootCommand("Used to import CSVs into our Domain");

            command.AddArgument(new Argument<string>("Plant CSV Folder Path", "Path to the folder that contains the Plant Import CSVs"));
            command.AddArgument(new Argument<string>("Zipcode Hardiness CSV Folder Path", "Path to the folder that contains the Zipcode Hardiness CSVs"));

            command.SetHandler<string, string>(Convert);

            return await command.InvokeAsync(args);
        }

        public static void Convert(string plantCSVFolderPath, string zipcodeHardinessCSVFolderPath)
        {
            Console.WriteLine("Convert");
            // PlantImporter.Run(plantCSVFolderPath);
            // ZipcodeHardinessImporter.Run(zipcodeHardinessCSVFolderPath);
        }
    }
}
