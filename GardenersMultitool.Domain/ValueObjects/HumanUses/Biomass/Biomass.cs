using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Biomass
{
    public class Biomass : ValueObject, IBiomass
    {
        public string Label => "Erosion Control";

        public static IBiomass Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Biomass" => new Biomass(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
