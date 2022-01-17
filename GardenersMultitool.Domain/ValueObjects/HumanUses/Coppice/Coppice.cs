using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Coppice
{
    public class Coppice : ValueObject, ICoppice, IHumanUseFactory
    {
        public string Label => "Coppice";

        public static ICoppice Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "coppice" => new Coppice(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
