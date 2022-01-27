using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class GetLocationById : IRequest<Location>
    {
        public string Id { get; }

        public GetLocationById(string id)
        {
            Id = id;
        }
    }

    public class GetRequestByIdHandler : RequestHandler<GetLocationById, Location>
    {
        public GetRequestByIdHandler(DataContext context) : base(context)
        {
        }

        public override async Task<Location> Handle(GetLocationById request, CancellationToken cancellationToken) =>
            await Context.Locations
                .Find(location => location.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
