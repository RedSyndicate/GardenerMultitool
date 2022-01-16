using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.HumanUses;

namespace PlantDataImporter
{
    public static class PlantMapper
    {
        public static Plant Map(PlantDto plantDto)
        {
            var plant = new Plant();
            plant.ScientificName = plantDto.ScientificName;
            plant.EcologicalFunction = plantDto.EcologicalFunction.Split(',')
                .Select(x => x.Trim())
                .Aggregate(new List<IEcologicalFunction>(), AggregateEcologicalFunctions);
            plant.HumanUse = plantDto.HumanUseCrop.Split(',')
                .Select(x => x.Trim())
                .Aggregate(new List<IHumanUse>(), AggregateHumanUses);
            plant.Binomial = plantDto.Binomial;
            if(!string.IsNullOrEmpty(plantDto.SoilPH))
                plant.SoilPH = plantDto.SoilPH.Split('-').Select(x => x.Trim()).topH();
            //TODO: mappy mappy
            return plant;
        }

        private static Maybe<pH> topH(this IEnumerable<string> soilpHTokens) => 
            soilpHTokens.Count() < 2 
                ? Maybe.None
                : new pH(decimal.Parse(soilpHTokens.ElementAt(0)), decimal.Parse(soilpHTokens.ElementAt(1)));

        private static List<IEcologicalFunction> AggregateEcologicalFunctions(List<IEcologicalFunction> list, string function)
        {
            list.Add(EcologicalFunctions.Create(function));
            return list;
        }

        private static List<IHumanUse> AggregateHumanUses(List<IHumanUse> list, string humanUse)
        {
            list.Add(HumanUses.Create(humanUse));
            return list;
        }
    }
}

