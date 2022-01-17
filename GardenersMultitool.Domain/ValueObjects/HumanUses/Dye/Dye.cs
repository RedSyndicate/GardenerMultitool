using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Dye
{
    public class Dye : ValueObject, IDye, IHumanUseFactory
    {
        public string Label => "Dye";

        public static IDye Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "dye" => new Dye(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
