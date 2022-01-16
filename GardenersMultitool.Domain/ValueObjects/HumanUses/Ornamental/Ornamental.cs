
using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Ornamental
{
    public class Ornamental : ValueObject, IOrnamental
    {
        public string Label => "Ornamental";

        public static IOrnamental Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Ornamental" => new Ornamental(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
