using GardenersMultitool.Domain.Helpers;
using Shouldly;
using Xunit;

namespace GardenersMultitool.Domain.Tests.Common
{
    public class TemperatureTests
    {
        [Fact]
        public void BasicTemperatureOperators()
        {
            var temperature1 = new Temperature(27, TemperatureUnit.Celsius);
            var temperature2 = new Temperature(15, TemperatureUnit.Celsius);
            var subtraction = temperature1 - temperature2;
            subtraction.Value.ShouldBe(27 - 15);
            subtraction.ShouldBe(new Temperature(12, TemperatureUnit.Celsius));

            var additions = temperature1 + temperature2;

            additions.Value.ShouldBe(27 + 15);
            additions.ShouldBe(new Temperature(42, TemperatureUnit.Celsius));
        }

        [Fact]
        public void DifferentUnitsTemperatureOperators()
        {
            var temperature1 = new Temperature(84, TemperatureUnit.Farenheit);
            var temperature2 = new Temperature(15, TemperatureUnit.Celsius);
            var subtraction = temperature1 - temperature2;
            subtraction.Value.ShouldBe(84 - 59);
            subtraction.ShouldBe(new Temperature(84-59, TemperatureUnit.Farenheit));

            var additions = temperature1 + temperature2;

            additions.Value.ShouldBe(84 + 59);
            additions.ShouldBe(new Temperature(84 + 59, TemperatureUnit.Farenheit));
        }
        [Fact]
        public void CelsiusConvertsToFarenheit()
        {
            var temperature1 = new Temperature(27, TemperatureUnit.Celsius);
            var temperature2 = new Temperature(15, TemperatureUnit.Celsius);

            var f1 = temperature1.ConvertToFarenheit();
            var f2 = temperature2.ConvertToFarenheit();

            f1.ShouldSatisfyAllConditions(
                temp => temp.Unit.ShouldBe(TemperatureUnit.Farenheit),
                temp => temp.Value.ShouldBe(80.6m));

            f2.ShouldSatisfyAllConditions(
                temp => temp.Unit.ShouldBe(TemperatureUnit.Farenheit),
                temp => temp.Value.ShouldBe(59m));
        }

        [Fact]
        public void FarenheitConvertsToCelsius()
        {
            var temperature1 = new Temperature(-30, TemperatureUnit.Farenheit);
            var temperature2 = new Temperature(85, TemperatureUnit.Farenheit);

            var f1 = temperature1.ConvertToCelsius();
            var f2 = temperature2.ConvertToCelsius();

            f1.ShouldSatisfyAllConditions(
                temp => temp.Unit.ShouldBe(TemperatureUnit.Celsius),
                temp => temp.Value.ShouldBeInRange(-34.4445m, -34.4444m));

            f2.ShouldSatisfyAllConditions(
                temp => temp.Unit.ShouldBe(TemperatureUnit.Celsius),
                temp => temp.Value.ShouldBeInRange(29.4444m, 29.4445m));
        }

    }
}
