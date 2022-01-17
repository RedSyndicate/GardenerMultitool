using System;

namespace GardenersMultitool.Domain.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
