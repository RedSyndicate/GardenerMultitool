using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class GetAllLocations : IRequest<IEnumerable<Location>>
    {
    }
    public class GetAllRequestsHandler : RequestHandler<GetAllLocations, IEnumerable<Location>>
    {
        public GetAllRequestsHandler(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Location>> Handle(GetAllLocations request, CancellationToken cancellationToken) =>
            await Context.Locations.Find(location => true).ToListAsync(cancellationToken);

    }
}
