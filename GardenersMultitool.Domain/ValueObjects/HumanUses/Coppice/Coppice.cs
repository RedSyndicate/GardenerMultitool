using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Coppice
{
    public class ContainerGarden : ValueObject, ICoppice
    {
        public string Label => "Coppice";

        public static ICoppice Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Coppice" => new ContainerGarden(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
