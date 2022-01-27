using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Api.UseCases.Locations;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace GardenersMultitool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IMediator _mediator;

        public LocationController(ILogger<LocationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{locationId}")]
        public async Task<Location> GetLocation(string locationId) => await _mediator.Send(new GetLocationById(locationId));

        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocation() => await _mediator.Send(new GetAllLocations());

        [HttpPost]
        public async Task<string> CreateLocation([FromBody] CreateNewLocation request) =>
            await _mediator.Send(request, CancellationToken.None);

        [HttpPut("{locationId}/add_plants")]
        public async Task<Location> AddPlants(string locationId, [FromBody] List<string> plantIds) =>
            await _mediator.Send(new AddPlantsToLocation(plantIds, locationId), CancellationToken.None);

        [HttpGet("recommendations")]
        public async Task<IEnumerable<Plant>> Recommendations(Guid locationId) => await _mediator.Send(new RecommendByLocation(locationId));

        [HttpPost("hardiness_zone")]
        public async Task<Guid> FetchAndUpdateHardinessZone(UpdateLocationHardiness request) => await _mediator.Send(request);
    }

}

