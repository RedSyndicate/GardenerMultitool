using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.PlantCharacteristics.Tolerances
{
    public class Tolerances
    {
        public ITolerance Create(string sunRequirementStr) =>
            sunRequirementStr.ToLowerInvariant() switch
            {
                "Drought" => new Drought(),
                "Flood" => new Flood(),
                "Salt" => new Salt(),
                _ => throw new ArgumentException()
            };
    }
    public class Drought: ValueObject, ITolerance
    {
        public string Label => "Drought";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Flood: ValueObject, ITolerance
    {
        public string Label => "Flood";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Salt: ValueObject, ITolerance
    {
        public string Label => "Salt";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

}
