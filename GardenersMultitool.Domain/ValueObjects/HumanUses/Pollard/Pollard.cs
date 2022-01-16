

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Pollard
{
    public class Pollard : ValueObject, IPollard
    {
        public string Label => "Pollard";

        public static IPollard Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Pollard" => new Pollard(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
