using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases
{
    public class CreateNewLocation : IRequest<string>
    {
        public string Name { get; set; }
        public int HardinessZone { get; set; }
    }

    public class CreateNewLocationHandler : Locations.RequestHandler<CreateNewLocation, string>
    {
        public CreateNewLocationHandler(DataContext context) : base(context) { }

        public override async Task<string> Handle(CreateNewLocation request, CancellationToken cancellationToken)
        {
            var location = Location.Create(request.Name, request.HardinessZone);
            await Context.Locations.InsertOneAsync(location, cancellationToken: cancellationToken);
            return location.Id;
        }
    }
}
