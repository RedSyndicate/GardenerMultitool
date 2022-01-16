using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.ErosionControl
{
    public class ErosionControl : ValueObject, IErosionControl
    {
        public string Label => "Erosion Control";

        public static IErosionControl Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "erosion control" => new ErosionControl(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
