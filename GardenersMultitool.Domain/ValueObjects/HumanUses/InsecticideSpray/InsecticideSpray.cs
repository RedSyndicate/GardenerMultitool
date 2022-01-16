

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.InsecticideSpray
{
    public class InsecticideSpray : ValueObject, IInsecticideSpray
    {
        public string Label => "Insecticide, Spray";

        public static IInsecticideSpray Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Insecticide, Spray" => new InsecticideSpray(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
