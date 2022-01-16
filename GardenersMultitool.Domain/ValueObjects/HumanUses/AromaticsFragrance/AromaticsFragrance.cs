
using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.AromaticsFragrance
{
    public class AromaticsFragrance : ValueObject, IAromaticsFragrance
    {
        public string Label => "Aromatics/Fragrance";

        public static IAromaticsFragrance Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Aromatics/Fragrance" => new AromaticsFragrance(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
