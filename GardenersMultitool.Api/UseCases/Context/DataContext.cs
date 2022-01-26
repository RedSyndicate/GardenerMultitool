using System;
using System.Linq;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
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

            InitializeMappings();
        }

        private void InitializeMappings()
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), new GuidGenerator());
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));


            var types = typeof(IPlantAttribute).Assembly.GetTypes().Where(t => t.IsClass && t.IsAssignableTo(typeof(IPlantAttribute)));

            foreach (var t in types)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(t));
            }

            BsonClassMap.RegisterClassMap<pH>(map =>
            {
                map.AutoMap();
                map.MapCreator(ph => new pH(ph.MinimumpH, ph.MaximumpH));
            });

            BsonClassMap.RegisterClassMap<Plant>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(p => p.Id)
                    .SetIdGenerator(new GuidGenerator());
            });

            BsonClassMap.RegisterClassMap<Location>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(l => l.Id)
                    .SetIdGenerator(new GuidGenerator());
            });
        }

        public IMongoCollection<T> Collection<T>() where T : IAggregateRoot
        {
            if (typeof(T) == typeof(Location)) return (IMongoCollection<T>)_locations.Collection;
            if (typeof(T) == typeof(Plant)) return (IMongoCollection<T>)_plants.Collection;
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
            .GetCollection<T>(typeof(T).Name.ToLowerInvariant());

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

