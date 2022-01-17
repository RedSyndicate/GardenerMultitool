using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.CutFlower
{
    public class CutFlower : ValueObject, ICutFlower, IHumanUseFactory
    {
        public string Label => "Cut Flower";

        public static ICutFlower Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "cut flower" => new CutFlower(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
