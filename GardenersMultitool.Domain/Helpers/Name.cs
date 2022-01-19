using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.Helpers;

namespace GardenersMultitool.Domain.ValueObjects.Common
{
    public class Name : ValueObject, IName
    {
        public string Value { get; private set; }

        public Name(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(value);
            }

            Value = value;
        }

        public static implicit operator string(Name d) => d.Value;
        public static explicit operator Name(string b) => new(b);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static IName Create(string name)
        {
             return new Name(name);
        }
    }
}
