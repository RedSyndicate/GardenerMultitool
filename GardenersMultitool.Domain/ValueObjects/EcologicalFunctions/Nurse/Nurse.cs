using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Nurse
{
    public class Nurse : ValueObject, INurse, IEcologicalFunctionFactory
    {
        public string Label => "Ground Cover";

        public static INurse Create(string nurse) =>
            nurse.ToLowerInvariant() switch
            {
                "nurse" => new Nurse(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
