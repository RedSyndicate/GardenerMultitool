using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.SunRequirements
{
    public static class SunRequirements
    {
<<<<<<< HEAD
        public static ISunRequirement Create(string sunRequirementStr) =>
=======
        public ISunRequirements Create(string sunRequirementStr) =>
>>>>>>> Filled out more of the data model
            sunRequirementStr.ToLowerInvariant() switch
            {
                "full_sun" => new FullSun(),
                "partial_shade" => new PartialShade(),
                "shade" => new Shade(),
                _ => throw new ArgumentException()
            };
    }
    public class FullSun : ValueObject, ISunRequirements
    {
        public string Label => "Full Sun";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class PartialShade : ValueObject, ISunRequirements
    {
        public string Label => "Partial Shade";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Shade : ValueObject, ISunRequirements
    {
        public string Label => "Shade";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
