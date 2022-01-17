
using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.AromaticsFragrance
{
    public class AromaticsFragrance : ValueObject, IAromaticsFragrance, IHumanUseFactory
    {
        public string Label => "Aromatics/Fragrance";

        public static IAromaticsFragrance Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "aromatics/fragrance" => new AromaticsFragrance(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
