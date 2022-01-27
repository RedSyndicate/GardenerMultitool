using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace GardenersMultitool.Api.CustomConfigurations
{
    public static class PlantCache
    {
        public static IServiceCollection AddPlantCache(this IServiceCollection collection, string directory) =>
            collection.AddSingleton(_ => new Loader()
                .Run("Permaculture_Plant_CSVs", directory)
                .ToDictionary(plant => plant.PlantId));
    }
}

