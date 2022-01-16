using CsvHelper;
using GardenersMultitool.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PlantDataImporter
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;


            //make list of file names (Annuals, aquatics, etc) iterate through list[]
            //var csvFolder = @"D:\ZainGit\RedSyndicateStuff\PermacultureData\CSV";
            //var jsonFolder = @"D:\ZainGit\RedSyndicateStuff\PermacultureData\JSON";
            var csvFolder = args[0];
            var jsonFolder = args[1];

            var files = Directory.GetFiles(csvFolder);

            //make records list
            var plants = new List<PlantDto>();


            //json serialize the records list (collection)
            foreach (var file in files)
            {
                using var reader = new StreamReader(Path.Combine(csvFolder, file));
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<PlantDto>();
                foreach (var record in records)
                {
                    plants.Add(record);
                    Console.WriteLine($"{record.Name}");                       
                }
            }

            var json = JsonConvert.SerializeObject(plants);
            var allPlants = JsonConvert.DeserializeObject<List<PlantDto>>(json);


            File.WriteAllText(Path.Combine(jsonFolder, "plants.json"), json);


            Console.WriteLine(allPlants[0].ToString());
            Console.WriteLine(allPlants[1].ToString());
            Console.WriteLine(allPlants[2].ToString());

            Console.ReadLine();



            //Next Step: For each different value of a property (like PlantType), list out each one once, and log it to console, so I can get a list of all enums to add to .cs files

        }




    }
}
