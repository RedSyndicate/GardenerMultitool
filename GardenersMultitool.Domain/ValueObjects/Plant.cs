using CSharpFunctionalExtensions;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Drawing;

namespace GardenersMultitool.Domain.ValueObjects
{
    public class Plant : ValueObject
    {
        //Properties modelled after .csv
        public int Id { get; set; }
        public string Name { get; set; }
        [Name("Scientific name")]
        public string ScientificName { get; set; }
        public string Binomial { get; set; }
        [Name("Plant Type")]
        public string PlantType { get; set; }
        public string Height { get; set; }
        public string Spread { get; set; }
        [Name("Root Depth")]
        public string RootDepth { get; set; }
        [Name("Seasonal Interest")]
        public string SeasonalInterest { get; set; }
        public string Notes { get; set; }
        [Name("Flower Color")]
        public string FlowerColor { get; set; }
        [Name("Root Type")]
        public string RootType { get; set; }
        [Name("Bloom Time")]
        public string BloomTime { get; set; }
        [Name("Fruit Time")]
        public string FruitTime { get; set; }
        public string Texture { get; set; }
        public string Form { get; set; }
        [Name("Growth Rate")]
        public string GrowthRate { get; set; }
        [Name("Insect Predation")]
        public string InsectPredation { get; set; }
        public string Disease { get; set; }
        [Name("Light")]
        public string LightRequired{ get; set; }
        [Name("Hardiness Zone")]
        public string HardinessZone { get; set; }
        [Name("Soil Moisture")]
        public string SoilMoisture { get; set; }
        [Name("Soil pH")]
        public string SoilPH { get; set; }
        [Name("Ecological Function")]
        public string EcologicalFunction { get; set; }
        [Name("Human Use/Crop")]
        public string HumanUseCrop { get; set; }

        public PlotNeeds Functions { get; }

        public override string ToString()
        {
            return $"Object Id:{Id}, Name: {Name}.";
        }

        public Plant()
        {

        }

        public Plant(string name, PlotNeeds needs)
        {
            Name = name;
            Functions = needs;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            //yield return Height;
            //yield return ScientificName;
            //yield return Binomial;
            //yield return PlantType;
            //yield return Height;
            //yield return Spread;
            //yield return RootDepth;
            //yield return SeasonalInterest;
            //yield return Notes;
            //yield return FlowerColor;
            //yield return RootType;
            //yield return BloomTime;
            //yield return FruitTime;
            //yield return Texture;
            //yield return Form;
            //yield return GrowthRate;
            //yield return InsectPredation;
            //yield return Disease;
            //yield return LightRequired;
            //yield return HardinessZone;
            //yield return SoilMoisture;
            //yield return SoilPH;
            //yield return EcologicalFunction;
            //yield return HumanUseCrop;
            //yield return PlotNeeds;

        }
    }
}
