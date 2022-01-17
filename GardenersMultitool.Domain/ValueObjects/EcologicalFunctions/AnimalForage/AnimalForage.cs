using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.AnimalForage
{
    public class AnimalForage : ValueObject, IAnimalForage, IEcologicalFunctionFactory
    {
        public string Label => "Animal Forage";

        public static IAnimalForage Create(string animalForage) => 
            animalForage.ToLowerInvariant() switch 
            {
                "domestic animal forage" => new AnimalForage(),
                _ => throw new ArgumentException(animalForage)
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
