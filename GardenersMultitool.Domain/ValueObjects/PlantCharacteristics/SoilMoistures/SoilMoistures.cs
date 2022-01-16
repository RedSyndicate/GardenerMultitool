using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.PlantCharacteristics.SoilMoistures
{
    public class SoilMoistures
    {
        public ISoilMoisture Create(string sunRequirementStr) =>
            sunRequirementStr.ToLowerInvariant() switch
            {
                "Wet" => new Wet(),
                "Moderate" => new Moderate(),
                "Dry" => new Dry(),
                _ => throw new ArgumentException()
            };
    }
    public class Wet : ValueObject, ISoilMoisture
    {
        public string Label => "Wet";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Moderate : ValueObject, ISoilMoisture
    {
        public string Label => "Moderate";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Dry : ValueObject, ISoilMoisture
    {
        public string Label => "Dry";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

}
