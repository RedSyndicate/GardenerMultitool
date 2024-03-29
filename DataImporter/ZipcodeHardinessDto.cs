﻿using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace DataImporter
{
    public class ZipcodeHardinessZoneDto
    {
        //Properties modelled after .csv
        [Name("zipcode")]
        public string Zipcode { get; set; }
        [Name("zone")]
        public string Zone { get; set; }
        [Name("trange")]
        public string TemperatureRange { get; set; }
        [Name("zonetitle")]
        public string ZoneTitle { get; set; }

        public override string ToString()
        {
            return $"Zipcode:{Zipcode}, Hardiness Zone: {Zone}. Temperature Lows: {TemperatureRange}";
        }
    }
}
