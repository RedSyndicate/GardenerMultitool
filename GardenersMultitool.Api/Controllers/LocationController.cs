using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Locations;
using GardenersMultitool.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GardenersMultitool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly IMediator _mediator;

        public LocationsController(ILogger<LocationsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Location>> GetLocations() => await _mediator.Send(new GetAllLocations());

        [HttpGet("by/locationId/{locationId:guid}")]
        public async Task<Location> GetLocation(Guid locationId) => await _mediator.Send(new GetLocationById(locationId));

        [HttpPost("create")]
        public async Task<Guid> CreateLocation([FromBody] CreateNewLocation request) =>
            await _mediator.Send(request, CancellationToken.None);

        [HttpPut("{locationId:guid}/add_plants")]
        public async Task<Location> AddPlants(Guid locationId, [FromBody] List<Guid> plantIds) =>
            await _mediator.Send(new AddPlantsToLocation(plantIds, locationId), CancellationToken.None);

        [HttpPost("{locationId:guid}/hardiness_zone/{zipcode}")]
        public async Task<Guid> FetchAndUpdateHardinessZone(Guid locationId, string zipcode) => await _mediator.Send(new UpdateLocationHardiness(locationId, zipcode));

        [HttpGet("{locationId:guid}/recommendations")]
        public async Task<IEnumerable<Plant>> Recommendations(Guid locationId) => await _mediator.Send(new RecommendByLocation(locationId));
    }

}

