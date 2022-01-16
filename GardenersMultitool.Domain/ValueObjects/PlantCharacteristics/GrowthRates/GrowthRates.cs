using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.PlantCharacteristics.GrowthRates
{
    public class GrowthRates
    {
        public IGrowthRate Create(string sunRequirementStr) =>
            sunRequirementStr.ToLowerInvariant() switch
            {
                "Slow" => new Slow(),
                "Fast" => new Fast(),
                "Moderate" => new Moderate(),
                _ => throw new ArgumentException()
            };
    }
    public class Slow : ValueObject, IGrowthRate
    {
        public string Label => "Slow";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Fast : ValueObject, IGrowthRate
    {
        public string Label => "Fast";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Moderate : ValueObject, IGrowthRate
    {
        public string Label => "Moderate";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

}
