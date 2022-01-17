using System.Collections.Generic;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Wildlife;
using GardenersMultitool.Domain.ValueObjects.PlantCharacteristics.SunRequirements;


namespace GardenersMultitool.Domain.Entities
{
    class Location
    {
        public List<Plant> Plants { get; set; }
        public string Name { get; set; }
        public HabitationZone HabitationZone { get; set; }
        public pH SoilpH { get; set; }
        public ISunRequirement SunRequirements { get; }
        public IWildlife Wildlife { get; set; }
        public decimal Area { get; set; }
        public bool Compaction { get; set; }

    }
}
