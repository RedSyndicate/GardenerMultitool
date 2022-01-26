using System;
using MongoDB.Bson;

namespace GardenersMultitool.Domain.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
