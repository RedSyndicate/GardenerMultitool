using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.DriedFlower
{
    public class DriedFlower : ValueObject, IDriedFlower, IHumanUseFactory
    {
        public string Label => "Dried Flower";

        public static IDriedFlower Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "dried flower" => new DriedFlower(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
