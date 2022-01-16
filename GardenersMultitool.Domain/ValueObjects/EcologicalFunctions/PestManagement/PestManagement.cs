using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.PestManagement
{
    public class PestManagement
    {
        public static IPestManagement Create(string animalForage) => 
            animalForage.ToLowerInvariant() switch 
            {
                "insecticide" => new Insecticide(),
                _ => throw new ArgumentException()
            };

    }

    public class Insecticide : ValueObject, IPestManagement
    {
        public string Label => "Insecticide";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    } 
}
