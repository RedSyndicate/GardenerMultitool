using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.SunRequirements
{
    public class SunRequirements
    {
        public ISunRequirement Create(string sunRequirementStr) =>
            sunRequirementStr.ToLowerInvariant() switch
            {
                "full_sun" => new FullSun(),
                "partial_shade" => new PartialShade(),
                "shade" => new Shade(),
                _ => throw new ArgumentException()
            };
    }
    public class FullSun : ValueObject, ISunRequirement
    {
        public string Label => "Full Sun";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class PartialShade : ValueObject, ISunRequirement
    {
        public string Label => "Partial Shade";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Shade : ValueObject, ISunRequirement
    {
        public string Label => "Shade";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
