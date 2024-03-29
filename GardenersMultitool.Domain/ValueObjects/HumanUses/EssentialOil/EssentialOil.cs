﻿using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.EssentialOil
{
    public class EssentialOil : ValueObject, IEssentialOil, IHumanUseFactory
    {
        public string Label => "Essential Oil";

        public static IEssentialOil Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "essential oil" => new EssentialOil(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
