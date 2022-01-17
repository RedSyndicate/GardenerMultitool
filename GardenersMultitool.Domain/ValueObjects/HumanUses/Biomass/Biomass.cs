using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Biomass
{
    public class Biomass : ValueObject, IBiomass, IHumanUseFactory
    {
        public string Label => "Biomass";

        public static IBiomass Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "biomass" => new Biomass(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
