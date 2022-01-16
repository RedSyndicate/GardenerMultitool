﻿
using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.Fiber
{
    public class Fiber : ValueObject, IFiber
    {
        public string Label => "Fiber";

        public static IFiber Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "Fiber" => new Fiber(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
