using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Compost
{
    public class Compost : ValueObject, IContainerGarden, IHumanUseFactory
    {
        public string Label => "Compost";

        public static IContainerGarden Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "compost" => new Compost(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
