using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.ChemicalDeterrents
{
    public static class ChemicalDeterrents
    {
        public static IChemicalDeterrent Create(string chemicalDeterrent) =>
            chemicalDeterrent.ToLowerInvariant() switch
            {
                "fungicide" => new Fungicide(),
                "herbicide" => new Herbicide(),
                _ => throw new ArgumentException()
            };
    }
    public class Fungicide : ValueObject, IChemicalDeterrent
    {
        public string Label => "Fungicide";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Herbicide : ValueObject, IChemicalDeterrent
    {
        public string Label => "Hedge";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
