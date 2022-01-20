using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GardenersMultitool.Domain.Entities
{
    public interface IEntity
    {
        public string Id { get; set; }
    }
}