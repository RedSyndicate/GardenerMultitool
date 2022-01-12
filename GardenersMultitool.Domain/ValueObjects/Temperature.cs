using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace GardenersMultitool.Domain.ValueObjects
{
    public enum TemperatureUnit
    {
        Farenheit,
        Celsius
    }

    public class Temperature : ValueObject
    {
        public decimal Value { get; }
        public TemperatureUnit Unit { get; }
     
        public Temperature(decimal value, TemperatureUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Unit;
        }
    }
}
