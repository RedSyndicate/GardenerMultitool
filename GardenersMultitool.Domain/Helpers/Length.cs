using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.Common
{
    public enum LengthUnit
    {
        Meters,
        Centimeters,
        Feet,
        Inches,
    }

    public class Length : ValueObject
    {
        public decimal Value { get; }
        public LengthUnit Unit { get; }

        public Length(decimal value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public Length ConvertToMeters() => Unit switch
        {
            LengthUnit.Meters => this,
            LengthUnit.Centimeters => new Length(Value*.01m, LengthUnit.Meters),
            LengthUnit.Feet => new Length(Value*0.3048m, LengthUnit.Meters),
            LengthUnit.Inches => new Length(Value*0.3048m*12, LengthUnit.Meters),
            _ => throw new ArgumentOutOfRangeException()
        };

        public Length ConvertToCentimeters() => Unit switch
        {
            LengthUnit.Meters => new Length(Value*100m, LengthUnit.Centimeters),
            LengthUnit.Centimeters => this,
            LengthUnit.Feet => new Length(Value*30.48m, LengthUnit.Centimeters),
            LengthUnit.Inches => new Length(Value*30.48m*12, LengthUnit.Centimeters),
            _ => throw new ArgumentOutOfRangeException()
        };

        public Length ConvertToFeet() => Unit switch
        {
            LengthUnit.Meters => new Length(Value*3.28084m, LengthUnit.Feet),
            LengthUnit.Centimeters => new Length(Value*.0328084m, LengthUnit.Feet),
            LengthUnit.Feet => this,
            LengthUnit.Inches => new Length(Value/12, LengthUnit.Feet),
            _ => throw new ArgumentOutOfRangeException()
        };
        public Length ConvertToInches() => Unit switch
        {
            LengthUnit.Meters => new Length(Value*12*3.28084m, LengthUnit.Feet),
            LengthUnit.Centimeters => new Length(Value*12*.0328084m, LengthUnit.Feet),
            LengthUnit.Feet => new Length(Value*12, LengthUnit.Feet),
            LengthUnit.Inches => this,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Unit;
            yield return Value;
        }
    }
}

