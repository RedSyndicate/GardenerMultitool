using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GardenersMultitool.Domain.ValueObjects.AnimalResistances
{
    public class AnimalResistances
    {
        public IAnimalResistance Create(string sunRequirementStr) =>
            sunRequirementStr.ToLowerInvariant() switch
            {
                "Deer" => new Deer(),
                "Rabbit" => new Rabbit(),
                "Gopher" => new Gopher(),
                "Mice" => new Mice(),
                _ => throw new ArgumentException()
            };
    }
    public class Deer: ValueObject, IAnimalResistance
    {
        public string Label => "Deer";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Rabbit: ValueObject, IAnimalResistance
    {
        public string Label => "Rabbit";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Gopher: ValueObject, IAnimalResistance
    {
        public string Label => "Gopher";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }
    public class Mice: ValueObject, IAnimalResistance
    {
        public string Label => "Mice";
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }
    }

}
