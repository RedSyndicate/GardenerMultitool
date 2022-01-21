using System.Collections.Generic;
using CSharpFunctionalExtensions;

// ReSharper disable InconsistentNaming

namespace GardenersMultitool.Domain.ValueObjects.Common
{
    public class pH : ValueObject
    {
        public decimal MinimumpH { get; } = 0;
        public decimal MaximumpH { get; } = 0;

        public pH(decimal minimumpH, decimal maximumpH)
        {
            MinimumpH = minimumpH;
            MaximumpH = maximumpH;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MinimumpH;
            yield return MaximumpH;
        }

        public override string ToString() => $"{MinimumpH} - {MaximumpH}";
    }
}
