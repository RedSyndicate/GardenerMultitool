using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GardenersMultitool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly Logger<LocationController> _logger;
        private readonly ConcurrentDictionary<Guid, Location> _locations;
        private readonly IDictionary<int, Plant> _ourPlants;

        public LocationController(Logger<LocationController> logger, ConcurrentDictionary<Guid, Location> locations, Dictionary<int, Plant> ourPlants)
        {
            _logger = logger;
            _locations = locations;
            _ourPlants = ourPlants;
        }

        [HttpGet("by_id")]
        public Location GetLocation(Guid id)
        {
            _locations.TryGetValue(id, out var location);
            return location;
        }

        [HttpGet]
        public IEnumerable<Location> GetLocation() => _locations.Values;


        [HttpPost]
        public Guid CreateLocation()
        {
            var location = Location.Create();
            _locations.TryAdd(location.Id, location);
            return location.Id;
        }

        [HttpPut("add_plants")]
        public ActionResult<Location> AddPlants(List<int> plantIds, Guid locationId)
        {
            if (!_locations.TryGetValue(locationId, out var location)) return location;
            var results = new List<Result<Plant>>();
            plantIds.ForEach(id =>
            {
                if (!_ourPlants.TryGetValue(id, out var plant)) return;
                results.Add(location.AddPlant(plant));
            });
            var result = results.Combine(" --- ");
            if (result.IsFailure) return BadRequest(result.Error);
            result.Match(_ => OnSuccess(location), OnFailure);
            return location;
        }

        private ActionResult<Location> OnSuccess(Location location) => Ok(location);
        private ActionResult<Location> OnFailure(string errors) => Ok(errors);

        [HttpPut("add_plant")]
        public Location AddPlant(int plantId, Guid locationId)
        {
            if (!_locations.TryGetValue(locationId, out var location)) return location;
            if (!_ourPlants.TryGetValue(plantId, out var plant)) return location;

            location.AddPlant(plant);
            return location;
        }

        [HttpGet("ecological_functions")]
        public IEnumerable<IPlantAttribute> EcologicalFunctions(Guid locationId)
        {
            _locations.TryGetValue(locationId, out var location);
            return location?.EcologicalFunctions.Distinct();
        }
    }
}

