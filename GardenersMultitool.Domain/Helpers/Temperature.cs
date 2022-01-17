using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.Common
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

        public Temperature ConvertToFarenheit() => Unit switch
        {
            TemperatureUnit.Farenheit => this,
            TemperatureUnit.Celsius => new Temperature(this*(9/5m)+32, TemperatureUnit.Farenheit),
            _ => throw new ArgumentOutOfRangeException()
        };

        public Temperature ConvertToCelsius() => Unit switch
        {
            TemperatureUnit.Farenheit => new Temperature((this - 32) * (5 / 9m), TemperatureUnit.Celsius),
            TemperatureUnit.Celsius => this,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static Temperature operator -(Temperature a, Temperature b) => a.Unit switch
        {
            TemperatureUnit.Farenheit => b.Unit switch
            {
                TemperatureUnit.Farenheit => new Temperature(a.Value - b.Value, TemperatureUnit.Farenheit),
                TemperatureUnit.Celsius => new Temperature(a.Value - b.ConvertToFarenheit().Value,
                    TemperatureUnit.Farenheit),
                _ => throw new ArgumentOutOfRangeException()
            },
            TemperatureUnit.Celsius => b.Unit switch
            {
                TemperatureUnit.Farenheit => new Temperature(a.Value - b.ConvertToCelsius().Value,
                    TemperatureUnit.Celsius),
                TemperatureUnit.Celsius => new Temperature(a.Value - b.Value, TemperatureUnit.Celsius),
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException()
        };
        public static Temperature operator +(Temperature a, Temperature b) => a.Unit switch
        {
            TemperatureUnit.Farenheit => b.Unit switch
            {
                TemperatureUnit.Farenheit => new Temperature(a.Value + b.Value, TemperatureUnit.Farenheit),
                TemperatureUnit.Celsius => new Temperature(a.Value + b.ConvertToFarenheit().Value,
                    TemperatureUnit.Farenheit),
                _ => throw new ArgumentOutOfRangeException()
            },
            TemperatureUnit.Celsius => b.Unit switch
            {
                TemperatureUnit.Farenheit => new Temperature(a.Value + b.ConvertToCelsius().Value,
                    TemperatureUnit.Celsius),
                TemperatureUnit.Celsius => new Temperature(a.Value + b.Value, TemperatureUnit.Celsius),
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException()
        };
        public static implicit operator decimal(Temperature d) => d.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Unit;
            yield return Value;
        }
    }
}
