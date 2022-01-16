using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.CutFlower
{
    public class CutFlower : ValueObject, ICutFlower
    {
        public string Label => "Cut Flower";

        public static ICutFlower Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Cut Flower" => new CutFlower(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
