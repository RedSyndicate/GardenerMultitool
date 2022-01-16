using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Food
{
    public class Food : ValueObject, IFood
    {
        public string Label => "Food";

        public static IFood Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Food" => new Food(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
