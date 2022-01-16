using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.ContainerGarden
{
    public class ContainerGarden : ValueObject, IContainerGarden
    {
        public string Label => "ContainerGarden";

        public static IContainerGarden Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Compost" => new ContainerGarden(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
