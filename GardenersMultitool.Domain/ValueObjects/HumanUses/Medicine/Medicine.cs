

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Medicine
{
    public class Medicine : ValueObject, IMedicine
    {
        public string Label => "Medicine";

        public static IMedicine Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Medicine" => new Medicine(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
