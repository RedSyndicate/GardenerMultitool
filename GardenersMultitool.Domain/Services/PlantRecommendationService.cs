using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;

namespace GardenersMultitool.Domain.Services
{
    public class PlantRecommendationService
    {
        private readonly Location _location;
        private readonly IEnumerable<Plant> _plants;

        private PlantRecommendationService(Location location, IEnumerable<Plant> plants)
        {
            _location = location;
            _plants = plants;
        }

        public static PlantRecommendationService Create(Maybe<Location> locationMaybe, Maybe<IEnumerable<Plant>> plantsMaybe)
        {
            var location = locationMaybe.GetValueOrThrow("Cannot make recommendations without a location");
            var plants = plantsMaybe.GetValueOrThrow("Cannot make recommendations without plants");
            return new PlantRecommendationService(location, plants);
        }

        public IEnumerable<Plant> Recommend()
        {
            // Converting these into value objects will help us keep logic out of this function and closer to the data.
            var hardiness = _location.HardinessZone;
            var soilPh = _location.SoilpH;
            var compaction = _location.Compaction;
            return _plants.Where(plant => plant.CompactionTolerated(compaction))
                .Where(plant => plant.SoilPHTolerated(soilPh))
                .Where(plant => plant.HardinessZoneTolerable(hardiness));
        }
    }
}

