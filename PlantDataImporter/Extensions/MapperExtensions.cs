using System.Linq;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.PlantType;

namespace PlantDataImporter.Extensions
{
    public static class MapperExtensions
    {
        public static Maybe<pH> TopH(this string[] soilpHTokens) => 
            soilpHTokens.Count() < 2 
                ? Maybe.None
                : new pH(decimal.Parse(soilpHTokens.ElementAt(0)), decimal.Parse(soilpHTokens.ElementAt(1)));

        public static IPlantType ToPlantType(this string plantType) =>
            PlantTypes.Create(plantType);

        public static IName ToName(this string name) =>
            Name.Create(name);
    }
}