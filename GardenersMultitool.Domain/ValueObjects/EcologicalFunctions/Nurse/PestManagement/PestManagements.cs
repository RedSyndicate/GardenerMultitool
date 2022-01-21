using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.PestManagement
{
    public class PestManagements : IEcologicalFunctionFactory
    {
        public static IPestManagement Create(string pestManagement) => 
            pestManagement.ToLowerInvariant() switch 
            {
                "insecticide" => new Insecticide(),
                "aromatic pest confuser" => new AromaticPestConfuser(),
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
    
    public class AromaticPestConfuser : ValueObject, IPestManagement
    {
        public string Label => "Aromatic Pest Confuser";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    } 

}

