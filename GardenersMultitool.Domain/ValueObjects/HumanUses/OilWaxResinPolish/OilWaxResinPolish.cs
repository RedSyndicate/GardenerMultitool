

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.OilWaxResinPolish
{
    public class OilWaxResinPolish : ValueObject, IOilWaxResinPolish
    {
        public string Label => "Oil, Wax, Resin, or Polish";

        public static IOilWaxResinPolish Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Oil, Wax, Resin, or Polish" => new OilWaxResinPolish(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
