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

        [Fact]
        public void Plot()
        {
            var plant = new Plant("Comfrey", PlotNeeds.NitrogenFixer | PlotNeeds.Shrub);
            PlotTemplates.Templates.TryGetValue(PlotTemplate.SoilFixer, out var plot);
            plot.AddPlant(plant);
            plot.Plants.Count.ShouldBe(1);
            plot.Needs.HasFlag(PlotNeeds.NitrogenFixer).ShouldBeFalse();
        }

        [Fact]
        public void Plot2()
        {
            var plant = new Plant("Comfrey", PlotNeeds.NitrogenFixer | PlotNeeds.Shrub);
            var plant2 = new Plant("BaconWillow", PlotNeeds.groundCover | PlotNeeds.Overstory);
            PlotTemplates.Templates.TryGetValue(PlotTemplate.SoilFixer, out var plot);
            plot.AddPlant(plant);
            plot.AddPlant(plant2);
            plot.Plants.Count.ShouldBe(2);
            plot.Needs.HasFlag(PlotNeeds.NitrogenFixer).ShouldBeFalse();
            plot.Needs.HasFlag(PlotNeeds.Overstory).ShouldBeFalse();
        }

    }
}
