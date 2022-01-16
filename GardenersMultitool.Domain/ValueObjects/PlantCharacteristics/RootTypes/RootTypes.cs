using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.PlantCharacteristics.RootTypes
{
    public class RootTypes
    {
        public IRootType Create(string sunRequirementStr) =>
            sunRequirementStr.ToLowerInvariant() switch
            {
                "Bulb" => new Bulb(),
                "Corm" => new Corm(),
                "Fibrous Deep" => new FibrousDeep(),
                "Fibrous Shallow" => new FibrousShallow(),
                "Long Rhizome" => new LongRhizome(),
                "Rhizome" => new Rhizome(),
                "Short Rhizome" => new ShortRhizome(),
                "Stolon" => new Stolon(),
                "Tap" => new Tap(),
                "Tuber" => new Tuber(),
                _ => throw new ArgumentException()
            };
    }
    public class Bulb : ValueObject, IRootType
    {
        public string Label => "Bulb";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Corm : ValueObject, IRootType
    {
        public string Label => "Corm";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class FibrousDeep: ValueObject, IRootType
    {
        public string Label => "Fibrous Deep";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class FibrousShallow : ValueObject, IRootType
    {
        public string Label => "Fibrous Shallow";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class LongRhizome: ValueObject, IRootType
    {
        public string Label => "Long Rhizome";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Rhizome : ValueObject, IRootType
    {
        public string Label => "Rhizome";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class ShortRhizome : ValueObject, IRootType
    {
        public string Label => "Short Rhizome";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Stolon : ValueObject, IRootType
    {
        public string Label => "Stolon";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Tap: ValueObject, IRootType
    {
        public string Label => "Tap";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Tuber: ValueObject, IRootType
    {
        public string Label => "Tuber";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }



}
