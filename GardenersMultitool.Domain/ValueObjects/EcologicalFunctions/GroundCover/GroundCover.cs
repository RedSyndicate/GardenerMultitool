using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.GroundCover
{
    public class GroundCover : ValueObject, IGroundCover, IEcologicalFunctionFactory
    {
        public string Label => "Ground Cover";

        public static IGroundCover Create(string groundCover) =>
            groundCover.ToLowerInvariant() switch
            {
                "groundcover" => new GroundCover(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
