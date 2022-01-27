using System;
using System.Collections.Generic;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.HabitationZone;
using GardenersMultitool.Domain.ValueObjects.PlantType;
using MongoDB.Bson.Serialization.Attributes;

namespace GardenersMultitool.Domain.Entities
{
    public class Plant : IAggregateRoot
    {
        [BsonId]
        public Guid Id { get; set; }
        public int PlantId { get; set; }
        public Name Name { get; set; }
        public Name ScientificName { get; set; }
        public string Binomial { get; set; }
        public IPlantType PlantType { get; set; }
        public string Height { get; set; }
        public string Spread { get; set; }
        public string RootDepth { get; set; }
        //build
        public string SeasonalInterest { get; set; }
        public string Notes { get; set; }
        //build
        public string FlowerColor { get; set; }
        //build
        public string RootType { get; set; }
        public string BloomTime { get; set; }
        public string FruitTime { get; set; }
        public string Texture { get; set; }
        public string Form { get; set; }
        public string GrowthRate { get; set; }
        public string InsectPredation { get; set; }
        public string Disease { get; set; }
        public string LightRequired { get; set; }
        public HardinessZoneRange HardinessZone { get; set; }
        public string SoilMoisture { get; set; }
        public pH SoilPH { get; set; }
        public List<IPlantAttribute> EcologicalFunction { get; set; }
        public List<IPlantAttribute> HumanUse { get; set; }

        public bool CompactionTolerated(bool compaction) => true;
        public bool SoilPHTolerated(pH soilPh) => SoilPH?.IsCompatible(soilPh) ?? true;
        public bool HardinessZoneTolerable(HardinessZone hardiness) => HardinessZone?.IsCompatible(hardiness) ?? true;
    }

}

