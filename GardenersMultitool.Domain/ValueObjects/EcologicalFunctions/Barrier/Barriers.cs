using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Barrier
{
    public class Barriers : IEcologicalFunctionFactory
    {
        public static IBarrier Create(string barrierStr) =>
            barrierStr.ToLowerInvariant() switch
            {
                "chemical barrier" => new ChemicalBarrier(),
                "hedge" => new Hedge(),
                "windbreak" => new WindBreak(),
                _ => throw new ArgumentException()
            };
    }
    public class ChemicalBarrier : ValueObject, IBarrier
    {
        public string Label => "Chemical Barrier";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Hedge : ValueObject, IBarrier
    {
        public string Label => "Hedge";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class WindBreak : ValueObject, IBarrier
    {
        public string Label => "Wind Break";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
