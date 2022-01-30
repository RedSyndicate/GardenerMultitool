using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using GardenersMultitool.Api.UseCases.Plants;
using GardenersMultitool.Domain.Entities;

namespace GardenersMultitool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        private readonly ILogger<PlantsController> _logger;
        private readonly IMediator _mediator;


        public PlantsController(ILogger<PlantsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Plant>> GetAllPlants() => await _mediator.Send(new GetAllPlants());

        [HttpGet("by/filter")]
        public async Task<IEnumerable<Plant>> GetPlantsByFilter([FromQuery] int total = 50, [FromQuery] int page = 1,
            [FromQuery] int perPage = 10) => await _mediator.Send(new GetAllPlants(total, page, perPage));

        [HttpGet("by/plantId/{plantId:int}")]
        public async Task<Plant> GetPlantByPlantId(int plantId) => await _mediator.Send(new GetPlantByPlantId(plantId));

        [HttpGet("by/plantType/{plantType}")]
        public async Task<IEnumerable<Plant>> GetPlantsByPlantType(string plantType, [FromQuery] int total = 50, [FromQuery] int page = 1, [FromQuery] int perPage = 10) => await _mediator.Send(new GetPlantsByPlantType(plantType, total, page, perPage));
    }
}
