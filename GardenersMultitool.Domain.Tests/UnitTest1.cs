using GardenersMultitool.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace GardenersMultitool.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestForJR()
        {
            var temperature1 = new Temperature(27, TemperatureUnit.Celsius);
            var temperature2 = new Temperature(15, TemperatureUnit.Celsius);
            var tempDelta = temperature1 - temperature2;
            tempDelta.Value.ShouldBe(27 - 15);
            tempDelta.ShouldBe(new Temperature(12, TemperatureUnit.Celsius));
        }
    }
}
