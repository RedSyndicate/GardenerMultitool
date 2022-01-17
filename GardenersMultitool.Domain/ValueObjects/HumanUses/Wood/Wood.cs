using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Wood
{
    public class Wood : ValueObject, IWood, IHumanUseFactory
    {
        public string Label => "Wood";

        public static IWood Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "wood" => new Wood(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
