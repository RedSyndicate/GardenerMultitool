using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions.Wildlife;
using GardenersMultitool.Domain.ValueObjects.PlantType;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GardenersMultitool.Api
{
    public class PlantService
    {
        private readonly IMongoCollection<Plant> _collection;

        public PlantService(IOptions<DatabaseSettings> settings)
        {
            var types = typeof(IPlantAttribute).Assembly.GetTypes().Where(t => t.IsClass && t.IsAssignableTo(typeof(IPlantAttribute)));

            foreach (var t in types)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(t));
            }

            BsonClassMap.RegisterClassMap<pH>(ph =>
            {
                ph.AutoMap();
                ph.MapCreator(ph => new pH(ph.MinimumpH, ph.MaximumpH));
            });

            BsonClassMap.RegisterClassMap<Plant>(p =>
            {
                p.AutoMap();
                p.MapCreator(p => new Plant());
                p.MapProperty(p => p.SoilPH);
            });

            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.Database);

            _collection = mongoDatabase.GetCollection<Plant>(typeof(Plant).Name.ToLower(), null);
        }
        public async Task<List<Plant>> GetAsync() =>
        await _collection.Find(_ => true).ToListAsync();

        public async Task<Plant?> GetAsync(int id) =>
            await _collection.Find(x => x.PlantId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Plant newBook) =>
            await _collection.InsertOneAsync(newBook);

        public async Task UpdateAsync(int id, Plant updatedBook) =>
            await _collection.ReplaceOneAsync(x => x.PlantId == id, updatedBook);

        public async Task RemoveAsync(int id) =>
            await _collection.DeleteOneAsync(x => x.PlantId == id);
    }
}
