﻿using System;
using System.Linq;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects.PlantType;

namespace GardenersMultitool.Domain.Extensions
{
    public static class MapperExtensions
    {
        public static pH TopH(this string[] soilpHTokens) =>
            soilpHTokens.Length < 2
                ? default
                : new pH(decimal.Parse(soilpHTokens.ElementAt(0)), decimal.Parse(soilpHTokens.ElementAt(1)));

        public static IPlantType ToPlantType(this string plantType) =>
            PlantTypes.Create(plantType);

        public static Zipcode ToZipcode(this string zipcode) =>
            new(zipcode.PadLeft(5, '0'));

        public static HardinessZone ToHardinessZone(this string hardinessZone) =>
            new(int.Parse(hardinessZone.Remove(1)));
        public static HardinessZoneRange ToHardinessZoneRange(this string[] tokens)
        {
            try
            {
                return tokens.Length < 3
                    ? default
                    : new HardinessZoneRange(
                        new HardinessZone(int.Parse(tokens[0])),
                        new HardinessZone(int.Parse(tokens[2])));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            return null;
        }
    }
}