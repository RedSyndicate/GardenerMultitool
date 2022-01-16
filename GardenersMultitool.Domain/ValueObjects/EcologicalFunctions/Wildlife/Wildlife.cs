using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Repellent
{
    public class Wildlife
    {
        public static IWildlife Create(string wildlife) => 
            wildlife.ToLowerInvariant() switch 
            {
                "insectory" => new Insectory(),
                "wildlife food" => new WildlifeFood(),
                "wildlife habitat" => new WildlifeHabitat(),
                _ => throw new ArgumentException()
            };
    }

    public class Insectory : ValueObject, IWildlife
    {
        public string Label => "Insectory";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class WildlifeFood : ValueObject, IWildlife
    {
        public string Label => "Wildlife Food";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class WildlifeHabitat : ValueObject, IWildlife
    {
        public string Label => "Wildlife Habitat";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}

