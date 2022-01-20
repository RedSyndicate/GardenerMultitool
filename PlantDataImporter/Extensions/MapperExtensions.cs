using System.Linq;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.PlantType;

namespace PlantDataImporter.Extensions
{
    public static class MapperExtensions
    {
        public static pH TopH(this string[] soilpHTokens) =>
            soilpHTokens.Count() < 2
                ? default(pH)
                : new pH(decimal.Parse(soilpHTokens.ElementAt(0)), decimal.Parse(soilpHTokens.ElementAt(1)));

        public static IPlantType ToPlantType(this string plantType) =>
            PlantTypes.Create(plantType);
    }
}