using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class AddPlantsToLocation : IRequest<Location>
    {
        public List<Guid> PlantIds { get; }
        public Guid LocationId { get; }

        public AddPlantsToLocation(List<Guid> plantIds, Guid locationId)
        {
            PlantIds = plantIds;
            LocationId = locationId;
        }

        public void Deconstruct(out List<Guid> plantIds, out Guid locationId)
        {
            plantIds = PlantIds;
            locationId = LocationId;
        }
    }

    public class AddPlantsToLocationRequestHandler : RequestHandler<AddPlantsToLocation, Location>
    {
        public override async Task<Location> Handle(AddPlantsToLocation request, CancellationToken cancellationToken)
        {
            var (plantIds, locationId) = request;
            var location = await Context.Collection<Location>().Find(location => location.Id == locationId).FirstOrDefaultAsync(cancellationToken);
            var plants = await Context.Collection<Plant>().Find(plant => plantIds.Contains(plant.Id)).ToListAsync();
            plants.ForEach(plant => location.AddPlant(plant));
            var updateDef = Builders<Location>.Update
                .Set(l => l.Plants, location.Plants);
            await Context.Collection<Location>()
                .UpdateOneAsync(l => l.Id == location.Id, updateDef, cancellationToken: cancellationToken);

            return location;
        }

        public AddPlantsToLocationRequestHandler(DataContext context) : base(context)
        {
        }
    }
}
