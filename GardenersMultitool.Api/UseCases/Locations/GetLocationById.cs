using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
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
        public GetLocationByIdHandler(DataContext context) : base(context)
        {
        }

        public async Task<Location> Handle(GetLocationById request, CancellationToken cancellationToken) =>
            await Context
                .Collection<Location>()
                .Find(location => location.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
