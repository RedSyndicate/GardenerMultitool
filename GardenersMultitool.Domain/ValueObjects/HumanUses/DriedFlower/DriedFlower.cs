using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects.HumanUses.CutFlower;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.DriedFlower
{
    public class DriedFlower : ValueObject, IDriedFlower
    {
        public string Label => "Dried Flower";

        public static IDriedFlower Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Dried Flower" => new DriedFlower(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
