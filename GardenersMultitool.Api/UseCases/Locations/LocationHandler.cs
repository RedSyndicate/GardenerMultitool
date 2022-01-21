using GardenersMultitool.Api.UseCases.Context;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public abstract class LocationHandler 
    {
        //protected readonly IMongoCollection<Location> Context;
        protected readonly DataContext Context;

        protected LocationHandler(DataContext context)
        {
            Context = context;
        }
    }


}

