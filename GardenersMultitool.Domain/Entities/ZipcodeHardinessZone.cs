using System;
using GardenersMultitool.Domain.Helpers;

namespace GardenersMultitool.Domain.Entities
{
    public class ZipcodeHardinessZone : IAggregateRoot
    {
        public ZipcodeHardinessZone(Zipcode zipcode, HardinessZone hardinessZone, string temperatureRange, string zoneTitle)
        {
            Zipcode = zipcode;
            HardinessZone = hardinessZone;
            TemperatureRange = temperatureRange;
            ZoneTitle = zoneTitle;
        }
        public ZipcodeHardinessZone() { }

        public HardinessZone HardinessZone { get; set; }
        public Zipcode Zipcode { get; set; }
        public string TemperatureRange { get; set; }
        public string ZoneTitle { get; set; }

        public Guid Id { get; set; }
    }
}
