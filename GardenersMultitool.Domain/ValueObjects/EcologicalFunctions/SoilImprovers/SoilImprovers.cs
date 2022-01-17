using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.SoilImprovers
{
    public class SoilImprovers : IEcologicalFunctionFactory
    {
        public static ISoilImprover Create(string soilImprover) => 
            soilImprover.ToLowerInvariant() switch 
            {
                "dynamic accumulator" => new DynamicAccumulator(),
                "mulch maker" => new MulchMaker(),
                "nitrogen fixer" => new NitrogenFixer(),
                "nitrogen scavenger" => new NitrogenScavenger(),
                "soil builder" => new SoilBuilder(),
                "soil cultivator" => new SoilCultivator(),
                _ => throw new ArgumentException()
            };
    }

    public class SoilCultivator : ValueObject, ISoilImprover
    {
        public string Label => "Soil Cultivator";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class DynamicAccumulator : ValueObject, ISoilImprover
    {
        public string Label => "Dynamic Accumulator";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class MulchMaker : ValueObject, ISoilImprover
    {
        public string Label => "Mulch Maker";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class NitrogenFixer : ValueObject, ISoilImprover
    {
        public string Label => "Nitrogen Fixer";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class NitrogenScavenger: ValueObject, ISoilImprover
    {
        public string Label => "Nitrogen Scavenger";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

    public class SoilBuilder: ValueObject, ISoilImprover
    {
        public string Label => "Soil Builder";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}

