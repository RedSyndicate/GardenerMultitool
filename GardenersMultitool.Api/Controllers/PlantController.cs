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
    public class PlantController : ControllerBase
    {
        private readonly ILogger<PlantController> _logger;
        private readonly IMediator _mediator;


        public PlantController(ILogger<PlantController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Plant>> GetPlants() => await _mediator.Send(new GetAllPlants());

        [HttpGet("{plantId:int}")]
        public async Task<Plant> GetPlantByPlantId(int plantId) => await _mediator.Send(new GetPlantByPlantId(plantId));

        [HttpGet("{plantType}")]
        public async Task<IEnumerable<Plant>> GetPlantsByPlantType(string plantType) => await _mediator.Send(new GetPlantsByPlantType(plantType));
    }
}
