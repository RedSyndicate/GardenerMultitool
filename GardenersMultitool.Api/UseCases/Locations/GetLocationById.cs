using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class GetLocationById : IRequest<Location>
    {
        public Guid Id { get; }

        public GetLocationById(Guid id)
        {
            Id = id;
        }
    }

    public class GetLocationByIdHandler : LocationHandler, IRequestHandler<GetLocationById, Location>
    {
        public GetLocationByIdHandler(IOptions<DatabaseSettings> settings) : base(settings)
        {
        }

        public async Task<Location> Handle(GetLocationById request, CancellationToken cancellationToken) =>
            await Context.Find(location => location.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
    }
}
