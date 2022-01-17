using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Restorers
{
    public class Restorers : IEcologicalFunctionFactory
    {
        public static IRestorer Create(string restorer) => 
            restorer.ToLowerInvariant() switch 
            {
                "air cleaner" => new AirCleaner(),
                "reclamator" => new Reclamator(),
                "toxin absorber" => new ToxinAbsorber(),
                "water purifier" => new WaterPurifier(),
                _ => throw new ArgumentException()
            };
    }

    public class WaterPurifier : ValueObject, IRestorer
    {
        public string Label => "Water Purifier";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class ToxinAbsorber : ValueObject, IRestorer
    {
        public string Label => "Toxin Absorber";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class Reclamator : ValueObject, IRestorer
    {
        public string Label => "Reclamator";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class AirCleaner: ValueObject, IRestorer
    {
        public string Label => "Air Cleaner";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

}

