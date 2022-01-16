using System.Linq;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects;

namespace PlantDataImporter.Extensions
{
    public static class MapperExtensions
    {
        public static Maybe<pH> topH(this string[] soilpHTokens) => 
            soilpHTokens.Count() < 2 
                ? Maybe.None
                : new pH(decimal.Parse(soilpHTokens.ElementAt(0)), decimal.Parse(soilpHTokens.ElementAt(1)));
    }
}