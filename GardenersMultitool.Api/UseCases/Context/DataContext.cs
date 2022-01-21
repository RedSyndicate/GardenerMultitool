using System;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Context
{
    public class DataContext
    {
        private readonly ICollectionProxy<Location> _locations;
        private readonly ICollectionProxy<Plant> _plants;
        public IMongoCollection<Location> Locations => _locations.Collection;
        public IMongoCollection<Plant> Plants => _plants.Collection;

        public DataContext(ICollectionProxy<Location> locations, ICollectionProxy<Plant> plants)
        {
            _locations = locations;
            _plants = plants;
        }

        public IMongoCollection<T> Collection<T>() where T : IAggregateRoot
        {
            if (typeof(T) == typeof(Location)) return (IMongoCollection<T>) _locations.Collection;
            if (typeof(T) == typeof(Plant)) return (IMongoCollection<T>) _plants.Collection;
            throw new ArgumentException($"Type {typeof(T)} is not mapped to a collection.");
        }
    }

    public interface ICollectionProxy<T> where T : IAggregateRoot
    {
        IMongoCollection<T> Collection { get; }
    }

    public abstract class CollectionProxy<T> : ICollectionProxy<T> where T : IAggregateRoot
    {
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;

        public IMongoCollection<T> Collection => _collection ??= _database
            .GetCollection<T>(nameof(T).ToLowerInvariant());

        protected CollectionProxy(IMongoDatabase database)
        {
            _database = database;
        }
    }

    public class LocationCollection : CollectionProxy<Location>
    {
        public LocationCollection(IMongoDatabase database) : base(database) { } 
    }
    public class PlantCollection : CollectionProxy<Plant>
    {
        public PlantCollection(IMongoDatabase database) : base(database) { } 
    }
}

