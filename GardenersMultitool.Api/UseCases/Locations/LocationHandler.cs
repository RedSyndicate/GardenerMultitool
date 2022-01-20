using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GardenersMultitool.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public abstract class LocationHandler 
    {
        protected readonly IMongoCollection<Location> Context;

        protected LocationHandler(IOptions<DatabaseSettings> settings)
        {
            Context = new MongoClient(settings.Value.ConnectionString)
                .GetDatabase(settings.Value.Database)
                .GetCollection<Location>(nameof(Location).ToLowerInvariant());
        }
    }
}

