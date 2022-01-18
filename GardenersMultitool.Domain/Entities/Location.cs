using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Wildlife;
using GardenersMultitool.Domain.ValueObjects.PlantCharacteristics.SunRequirements;


namespace GardenersMultitool.Domain.Entities
{
    public class Location : IAggregateRoot
    {
        public Guid Id { get; init; }
        public List<Plant> Plants { get; set; } = new();
        public string Name { get; set; }
        public HabitationZone HabitationZone { get; set; }
        public pH SoilpH { get; set; }
        public ISunRequirement SunRequirements { get; }
        public IWildlife Wildlife { get; set; }
        public decimal Area { get; set; }
        public bool Compaction { get; set; }

        public static Location Create() =>
            new() {Id = Guid.NewGuid()};

        public static Location Create(string name) =>
            new() {Id = Guid.NewGuid(), Name = name};

        public Result<Plant> AddPlant(Plant value)
        {
            //validation
            if (value == null)
                return Result.Failure<Plant>();
            Plants.Add(value);
            return value;
        }

        public IEnumerable<IPlantAttribute> EcologicalFunctions => Plants.SelectMany(x => x.EcologicalFunction);
    }
}
