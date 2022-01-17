

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Medicine
{
    public class Medicine : ValueObject, IMedicine, IHumanUseFactory
    {
        public string Label => "Medicine";

        public static IMedicine Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "medicine" => new Medicine(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
