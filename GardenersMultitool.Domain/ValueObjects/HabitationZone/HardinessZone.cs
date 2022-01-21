using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class HardinessZone : ValueObject
    {
        public int Zone { get; }

        public HardinessZone(int zone)
        {
            if (0 < Zone && Zone < 14)
                Zone = zone;
            else
                throw new ArgumentOutOfRangeException();
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Zone;
        }
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
    }
}
