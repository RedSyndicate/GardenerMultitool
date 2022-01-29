using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace GardenersMultitool.Domain.Helpers
{
    public class Zipcode : ValueObject
    {
        public string Value { get; set; }
        public string Route { get; set; }

        public Zipcode() { }
        public Zipcode(string value, string route = null)
        {
            if (value.Length != 5)
                throw new ArgumentException($"Error: Zipcode value not valid {value}");
            if (route != null && route.Length != 4)
                throw new ArgumentException($"Error: Zipcode+4 value not valid {route}");

            Value = value;
            Route = route;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
