

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.OilWaxResinPolish
{
    public class OilWaxResinPolish : ValueObject, IOilWaxResinPolish, IHumanUseFactory
    {
        public string Label => "Oil, Wax, Resin, or Polish";

        public static IOilWaxResinPolish Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "oil" => new OilWaxResinPolish(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
