using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;

namespace GardenersMultitool.Api.UseCases
{
    public class CreateNewLocation : IRequest<Guid> { }

    public class CreateNewLocationHandler : Locations.RequestHandler<CreateNewLocation, Guid>
    {
        public CreateNewLocationHandler(DataContext context) : base(context) { }

        public override async Task<Guid> Handle(CreateNewLocation request, CancellationToken cancellationToken)
        {
            var location = Location.Create();
            await Context.Locations.InsertOneAsync(Location.Create(), cancellationToken: cancellationToken);
            return location.Id;
        }
    }
}
