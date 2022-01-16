using System;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions
{
    public static class EcologicalFunctions
    {
        public static IEcologicalFunction Create(string function) => function switch
        {
            _ => throw new ArgumentException()
        };
    }
}