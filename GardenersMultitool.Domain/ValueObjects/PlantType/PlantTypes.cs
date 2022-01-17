using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.PlantType
{
    public interface IPlantTypeFactory
    {
    }

    public class PlantTypes : IPlantTypeFactory
    {
        public static IPlantType Create(string plantType) => plantType switch
        {
            "annual" => new Annual(),
            "aquatic" => new Aquatic(),
            "biennial" => new Biennial(),
            "deciduous shrub" => new DeciduousShrub(),
            "deciduous tree" => new DeciduousTree(),
            "evergreen shrub" => new EvergreenShrub(),
            "evergreen tree" => new EvergreenTree(),
            "fern" => new Fern(),
            "grass" => new Grass(),
            "mosses" => new Mosses(),
            "perennial" => new Perennial(),
            "vine" => new Vine(),
            _ => throw new ArgumentException(plantType)
        };
    }

    public class Vine : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Vine";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Perennial : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Perennial";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Mosses : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Mosses";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Grass : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Grass";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Fern : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Fern";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class EvergreenTree : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Evergreen Tree";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class EvergreenShrub : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Evergreen Shrub";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class DeciduousTree : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Deciduous Tree";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class DeciduousShrub : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Deciduous Shrub";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Biennial : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Biennial";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Aquatic : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Aquatic";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Annual : ValueObject, IPlantAttribute, IPlantType
    {
        public string Label => "Annual";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
}
