﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration.Attributes;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.PlantType;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class Plant : IAggregateRoot
    {
        [Ignore]
        public Guid Id { get; set; }
        public int PlantId { get; set; }
        public Name Name { get; set; }
        public Name ScientificName { get; set; }
        public string Binomial { get; set; }
        public IPlantType PlantType { get; set; }
        public string Height { get; set; }
        public string Spread { get; set; }
        public string RootDepth { get; set; }
        public string SeasonalInterest { get; set; }
        public string Notes { get; set; }
        public string FlowerColor { get; set; }
        public string RootType { get; set; }
        public string BloomTime { get; set; }
        public string FruitTime { get; set; }
        public string Texture { get; set; }
        public string Form { get; set; }
        public string GrowthRate { get; set; }
        public string InsectPredation { get; set; }
        public string Disease { get; set; }
        public string LightRequired { get; set; }
        public string HardinessZone { get; set; }
        public string SoilMoisture { get; set; }
        public pH SoilPH { get; set; }
        public List<IPlantAttribute> EcologicalFunction { get; set; }
        public List<IPlantAttribute> HumanUse { get; set; }

        public bool CompactionTolerated(bool compaction) => true;
        public bool SoilPHTolerated(pH soilPh) => true;
        public bool HardinessZoneTolerable(HardinessZone hardiness) => true;
    }

}
