using GardenersMultitool.Domain.ValueObjects;
using System.Collections.Generic;

namespace GardenersMultitool.Domain
{
    public class Plot
    {
        public List<Plant> Plants { get; } = new List<Plant>();

        public Plot() { }

        public Plant AddPlant(Plant plant)
        {
            Plants.Add(plant);
            return plant;
        }
    }
}
