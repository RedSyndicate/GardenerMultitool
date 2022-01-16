using System;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses
{
    public class HumanUses
    {
        public static IHumanUse Create(string function) => function switch
        {
            _ => throw new ArgumentException()
        };
    }
}
