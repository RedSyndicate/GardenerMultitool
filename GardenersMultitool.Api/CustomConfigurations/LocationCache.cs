using System;
using System.Collections.Concurrent;
using GardenersMultitool.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GardenersMultitool.Api.CustomConfigurations
{
    public static class LocationCache
    {
        public static IServiceCollection AddLocationCache(this IServiceCollection collection) =>
            collection.AddSingleton(_ =>
            {
                var dict = new ConcurrentDictionary<Guid, Location>();
                var location = Location.Create("Pond");
                dict.TryAdd(location.Id, location);
                return dict;
            });
    }
}
