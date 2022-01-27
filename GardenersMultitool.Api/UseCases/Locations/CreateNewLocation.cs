using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class CreateNewLocation : IRequest<Guid>
    {
        public string Name { get; set; }
        public int HardinessZone { get; set; }
    }

    public class CreateNewLocationHandler : RequestHandler<CreateNewLocation, Guid>
    {
        public CreateNewLocationHandler(DataContext context) : base(context) { }

        public override async Task<Guid> Handle(CreateNewLocation request, CancellationToken cancellationToken)
        {
            var location = Location.Create(request.Name, request.HardinessZone);
            await Context.Locations.InsertOneAsync(location, cancellationToken: cancellationToken);
            return location.Id;
        }
    }
}
