using System.Collections.Generic;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.HumanUses;
using GardenersMultitool.Domain.ValueObjects.PlantType;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class Plant
    {
        public int Id { get; set; }
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
        public string LightRequired{ get; set; }
        public string HardinessZone { get; set; }
        public string SoilMoisture { get; set; }
        public Maybe<pH> SoilPH { get; set; }
        public List<IEcologicalFunction> EcologicalFunction { get; set; }
        public List<IHumanUse> HumanUse { get; set; }
    }
}

