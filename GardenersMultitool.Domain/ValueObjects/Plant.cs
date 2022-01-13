using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class Plant : ValueObject
    {
        public string Name { get; }
        public PlotNeeds Functions { get; }

        public Plant(string name, PlotNeeds needs)
        {
            Name = name;
            Functions = needs;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
