using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Domain.Helpers;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class UpdateLocationHardiness : IRequest<Guid>
    {
        public Guid LocationId { get; set; }
        public string ZipCode { get; set; }

        public UpdateLocationHardiness(Guid locationId, string zipcode)
        {
            LocationId = locationId;
            ZipCode = zipcode;
        }
    }

    public class UpdateLocationHardinessHandler : RequestHandler<UpdateLocationHardiness, Guid>
    {
        public UpdateLocationHardinessHandler(DataContext context) : base(context)
        {

        }

        public override async Task<Guid> Handle(UpdateLocationHardiness request, CancellationToken cancellationToken)
        {
            var location = Context.Locations.Find(location => location.Id == request.LocationId).FirstOrDefault();
            var hardiness = HardinessFetchService(request.ZipCode);
            //location.UpdateHardiness(hardiness);
            throw new NotImplementedException();
        }

        private HardinessZone HardinessFetchService(string zipCode)
        {
            var thing = Context.ZipcodeHardinessZones.Find(hardyzips => hardyzips.Zipcode.Value == zipCode).FirstOrDefault();
            return new HardinessZone(thing.HardinessZone.Zone);
        }
    }
}
