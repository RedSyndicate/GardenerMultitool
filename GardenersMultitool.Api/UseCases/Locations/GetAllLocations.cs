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
    public class GetAllLocationsHandler : LocationHandler, IRequestHandler<GetAllLocations, IEnumerable<Location>>
    {
        public GetAllLocationsHandler(DataContext context) : base(context) { }

        public async Task<IEnumerable<Location>> Handle(GetAllLocations request, CancellationToken cancellationToken) =>
            await Context.Collection<Location>().Find(location => true).ToListAsync(cancellationToken);
            
    }
}
