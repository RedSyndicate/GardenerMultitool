using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public class UpdateLocationHardiness :IRequest<Guid>
    {
        public Guid LocationId { get; set; }
        public int ZipCode { get; set; }
        public UpdateLocationHardiness()
        {

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
            HardinessZone hardiness = HardinessFetchService(request.ZipCode);
            location.UpdateHardiness(hardiness);
            throw new NotImplementedException();
        }

        private HardinessZone HardinessFetchService(int zipCode)
        {
            int thing = Context.HardinessZipcodes.find(hardyzips => hardyzips.zipcode == zipCode).firstordefault()
            return new HardinessZone(thing);
        }
    }
}
