using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Soap
{
    public class Soap : ValueObject, ISoap, IHumanUseFactory
    {
        public string Label => "Soap";

        public static ISoap Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "soap" => new Soap(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
