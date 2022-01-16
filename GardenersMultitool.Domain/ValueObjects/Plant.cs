using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.HumanUses;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public string Binomial { get; set; }
        public string PlantType { get; set; }
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
        public string SoilPH { get; set; }
        public List<IEcologicalFunction> EcologicalFunction { get; set; }
        public List<IHumanUse> HumanUse { get; set; }
    }
}
