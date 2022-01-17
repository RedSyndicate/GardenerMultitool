using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.ContainerGarden
{
    public class ContainerGarden : ValueObject, IContainerGarden, IHumanUseFactory
    {
        public string Label => "Container Garden";

        public static IContainerGarden Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "container garden" => new ContainerGarden(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
