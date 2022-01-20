using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class GetAllLocations : IRequest<IEnumerable<Location>>
    {
    }
    public class GetAllLocationsHandler : LocationHandler, IRequestHandler<GetAllLocations, IEnumerable<Location>>
    {
        public GetAllLocationsHandler(IOptions<DatabaseSettings> settings) : base(settings) { }

        public async Task<IEnumerable<Location>> Handle(GetAllLocations request, CancellationToken cancellationToken) =>
            await Context.Find(location => true).ToListAsync(cancellationToken);
            
    }
}
