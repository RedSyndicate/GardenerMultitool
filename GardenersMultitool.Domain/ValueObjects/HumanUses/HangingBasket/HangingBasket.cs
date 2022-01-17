
using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.HangingBasket
{
    public class HangingBasket : ValueObject, IHangingBasket, IHumanUseFactory
    {
        public string Label => "Hanging Basket";

        public static IHangingBasket Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "hanging basket" => new HangingBasket(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
