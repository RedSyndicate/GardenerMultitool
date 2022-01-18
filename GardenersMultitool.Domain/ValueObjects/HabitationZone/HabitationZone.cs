using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class HabitationZone : ValueObject
    {
        public int Zone { get; }

        public HabitationZone(int zone)
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


}