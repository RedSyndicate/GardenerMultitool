using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Services;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class RecommendByLocation : IRequest<IEnumerable<Plant>>
    {
        public Guid LocationId { get; }

        public RecommendByLocation(Guid locationId)
        {
            LocationId = locationId;
        }
    }

    public class RecommendByLocationHandler : LocationHandler, IRequestHandler<RecommendByLocation, IEnumerable<Plant>>
    {
        public async Task<IEnumerable<Plant>> Handle(RecommendByLocation request, CancellationToken cancellationToken)
        {
            var plants = await Context.Collection<Plant>().Find(_ => true).ToListAsync();
            var location = await Context.Collection<Location>().Find(location => location.Id == request.LocationId).FirstOrDefaultAsync(cancellationToken);
            return PlantRecommendationService.Create(location, plants).Recommend();
        }

        public RecommendByLocationHandler(DataContext context) : base(context)
        {
        }
    }
}
