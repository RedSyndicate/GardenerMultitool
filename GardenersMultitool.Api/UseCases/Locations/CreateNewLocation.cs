using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Locations;
using GardenersMultitool.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases
{
    public class CreateNewLocation : IRequest<Guid> { }

    public class CreateNewLocationHandler : LocationHandler, IRequestHandler<CreateNewLocation, Guid>
    {

        public CreateNewLocationHandler(IOptions<DatabaseSettings> settings) : base(settings) { }

        public async Task<Guid> Handle(CreateNewLocation request, CancellationToken cancellationToken)
        {
            var location = Location.Create();
            await Context.InsertOneAsync(Location.Create(), cancellationToken: cancellationToken);
            return location.Id;
        }
    }
}
