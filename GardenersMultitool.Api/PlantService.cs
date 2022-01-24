using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.Common;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GardenersMultitool.Api
{
    public class PlantService
    {
        private readonly IMongoCollection<Plant> _collection;

        public PlantService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.Database);

            _collection = mongoDatabase.GetCollection<Plant>(nameof(Plant).ToLowerInvariant());
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
