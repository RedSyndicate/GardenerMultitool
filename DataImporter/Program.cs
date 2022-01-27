using System.ComponentModel;
using System.Data;

namespace DataImporter
{
    public class Program
    {
        private const string PlantFolderPath = "Permaculture_Plant_CSVs";
        private const string ZipcodeHardinessFolderPath = "Zipcode_Hardiness_CSVs";
        static void Main(string[] args)
        {
            //PlantImporter.Run(PlantFolderPath);
            ZipcodeHardinessImporter.Run(ZipcodeHardinessFolderPath);
        }
    }
}
