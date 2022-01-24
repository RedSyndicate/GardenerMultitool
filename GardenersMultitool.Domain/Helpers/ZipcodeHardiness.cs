using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects;
using System.Collections.Generic;

namespace GardenersMultitool.Domain.Helpers
{
    public class ZipcodeHardiness : ValueObject
    {
        public ZipcodeHardiness(HardinessZone hardinessZone, Zipcode zipcode, string temperatureRange, string zoneTitle)
        {
            HardinessZone = hardinessZone;
            Zipcode = zipcode;
            TemperatureRange = temperatureRange;
            ZoneTitle = zoneTitle;
        }

        public HardinessZone HardinessZone { get; }
        public Zipcode Zipcode { get; }
        public string TemperatureRange { get; }
        public string ZoneTitle { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HardinessZone;
            yield return Zipcode;
            yield return TemperatureRange;
            yield return ZoneTitle;
        }
    }
}
