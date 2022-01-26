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

        [HttpGet("{locationId:guid}")]
        public async Task<Location> GetLocation(Guid locationId) => await _mediator.Send(new GetLocationById(locationId));

        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocations() => await _mediator.Send(new GetAllLocations());

        [HttpPost]
        public async Task<Guid> CreateLocation([FromBody] CreateNewLocation request) =>
            await _mediator.Send(request, CancellationToken.None);

        [HttpPut("{locationId:guid}/add_plants")]
        public async Task<Location> AddPlants(Guid locationId, [FromBody] List<Guid> plantIds) =>
            await _mediator.Send(new AddPlantsToLocation(plantIds, locationId), CancellationToken.None);

        [HttpGet("{locationId:guid}/recommendations")]
        public async Task<IEnumerable<Plant>> Recommendations(Guid locationId) => await _mediator.Send(new RecommendByLocation(locationId));
    }

}

