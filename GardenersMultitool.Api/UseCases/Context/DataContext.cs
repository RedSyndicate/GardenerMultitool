using System;
using System.Linq;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Helpers;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.PlantType;
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
        private readonly ICollectionProxy<ZipcodeHardinessZone> _zipcodeHardiness;

        public IMongoCollection<Location> Locations => _locations.Collection;
        public IMongoCollection<Plant> Plants => _plants.Collection;
        public IMongoCollection<ZipcodeHardinessZone> ZipcodeHardinessZones => _zipcodeHardiness.Collection;

        public DataContext(ICollectionProxy<Location> locations, ICollectionProxy<Plant> plants, ICollectionProxy<ZipcodeHardinessZone> zipcodeHardiness)
        {
            _locations = locations;
            _plants = plants;
            _zipcodeHardiness = zipcodeHardiness;

            InitializeMappings();
        }

        private void InitializeMappings()
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), new GuidGenerator());
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));


            var plantAttributeTypes = typeof(IPlantAttribute).Assembly.GetTypes().Where(t => t.IsClass && t.IsAssignableTo(typeof(IPlantAttribute)));
            var plantTypes = typeof(IPlantType).Assembly.GetTypes().Where(t => t.IsClass && t.IsAssignableTo(typeof(IPlantType)));

            foreach (var t in plantTypes)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(t));
            }

            foreach (var t in plantAttributeTypes)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(t));
            }

            BsonClassMap.RegisterClassMap<pH>(map =>
            {
                map.AutoMap();
                map.MapCreator(ph => new pH(ph.MinimumpH, ph.MaximumpH));
            });

            BsonClassMap.RegisterClassMap<HardinessZoneRange>(map =>
            {
                map.AutoMap();
                map.MapCreator(hzr => new HardinessZoneRange(hzr.MaximumHardinessZone, hzr.MinimumHardinessZone));
            });

            BsonClassMap.RegisterClassMap<Plant>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(p => p.Id)
                    .SetIdGenerator(new GuidGenerator());
                map.AddKnownType(typeof(Annual));
            });

            BsonClassMap.RegisterClassMap<Location>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(l => l.Id)
                    .SetIdGenerator(new GuidGenerator());
            });

            BsonClassMap.RegisterClassMap<Zipcode>(map =>
            {
                map.AutoMap();
                map.MapCreator(z => new Zipcode(z.Value, z.Route));
            });

            BsonClassMap.RegisterClassMap<HardinessZone>(map =>
            {
                map.AutoMap();
                map.MapCreator(hz => new HardinessZone(hz.Zone));

            });

            BsonClassMap.RegisterClassMap<ZipcodeHardinessZone>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(zh => zh.Id)
                    .SetIdGenerator(new GuidGenerator());
            });
        }

        public IMongoCollection<T> Collection<T>() where T : IAggregateRoot
        {
            if (typeof(T) == typeof(Location)) return (IMongoCollection<T>)_locations.Collection;
            if (typeof(T) == typeof(Plant)) return (IMongoCollection<T>)_plants.Collection;
            if (typeof(T) == typeof(ZipcodeHardinessZone)) return (IMongoCollection<T>)_zipcodeHardiness.Collection;
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
    public class ZipcodeHardinessCollection : CollectionProxy<ZipcodeHardinessZone>
    {
        public ZipcodeHardinessCollection(IMongoDatabase database) : base(database) { }
    }
}

