using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.CleanserScourer
{
    public class CleanserScourer : ValueObject, ICleanserScourer, IHumanUseFactory
    {
        public string Label => "Cleanser/Scourer";

        public static ICleanserScourer Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "cleanser/scourer" => new CleanserScourer(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
