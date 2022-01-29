using System.Collections.Generic;

namespace GardenersMultitool.Domain.Entities
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
