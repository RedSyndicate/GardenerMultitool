using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.HabitationZone
{
    public class HardinessZone : ValueObject
    {
        public int Zone { get; }
        public bool NotKnown => Zone == 0;

        public HardinessZone(int zone)
        {
            if (zone is >= 0 and < 14)
                Zone = zone;
            else
                throw new ArgumentOutOfRangeException();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Zone;
        }

        public static bool operator<(HardinessZone a, HardinessZone b) => a.Zone < b.Zone;
        public static bool operator >(HardinessZone a, HardinessZone b) => a.Zone > b.Zone;
    }

    public class HardinessZoneRange : ValueObject
    {
        public HardinessZone MaximumHardinessZone { get; }
        public HardinessZone MinimumHardinessZone { get; }

        public HardinessZoneRange(HardinessZone maximumHardinessZone, HardinessZone minimumHardinessZone)
        {
            MaximumHardinessZone = maximumHardinessZone;
            MinimumHardinessZone = minimumHardinessZone;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MaximumHardinessZone;
            yield return MinimumHardinessZone;
        }

        public bool IsCompatible(HardinessZone hardiness) => MinimumHardinessZone < hardiness && hardiness < MaximumHardinessZone;
    }
}
