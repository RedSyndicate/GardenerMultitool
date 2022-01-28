using GardenersMultitool.Domain.Entities;
using System;

namespace GardenersMultitool.Domain.Helpers
{
    public class ZipcodeHardiness : IAggregateRoot
    {
        public ZipcodeHardiness(HardinessZone hardinessZone, Zipcode zipcode, string temperatureRange, string zoneTitle)
        {
            HardinessZone = hardinessZone;
            Zipcode = zipcode;
            TemperatureRange = temperatureRange;
            ZoneTitle = zoneTitle;
        }
        public ZipcodeHardiness() { }

        public HardinessZone HardinessZone { get; set; }
        public Zipcode Zipcode { get; set; }
        public string TemperatureRange { get; set; }
        public string ZoneTitle { get; set; }

        public Guid Id { get; set; }
    }
}
