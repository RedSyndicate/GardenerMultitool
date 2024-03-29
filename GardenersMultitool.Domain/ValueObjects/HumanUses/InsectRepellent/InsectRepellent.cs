﻿

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses.InsectRepellent
{
    public class InsectRepellent : ValueObject, IInsectRepellent, IHumanUseFactory
    {
        public string Label => "Insect Repellent";

        public static IInsectRepellent Create(string erosionControl) => 
            erosionControl.ToLowerInvariant() switch 
            {
                "insect repellent" => new InsectRepellent(),
                _ => throw new ArgumentException()
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
