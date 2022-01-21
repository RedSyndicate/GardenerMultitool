using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.Common;
using GardenersMultitool.Domain.ValueObjects.PlantType;

namespace GardenersMultitool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly ILogger<PlantController> _logger;
        private readonly PlantService _plantService;
        private readonly IDictionary<int, Plant> _plantCache;

        public PlantController(ILogger<PlantController> logger, PlantService dataService)
        {
            _logger = logger;
            _plantService = dataService;
        }

        [HttpGet]
        public async Task<Plant> GetById(int id)
        {
            return (await _plantService.GetAsync(id));
        }

        [HttpGet("annuals")]
        public async Task<IEnumerable<Plant>> GetAnnuals()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Annual);
        }

        [HttpGet("aquatic")]
        public async Task<IEnumerable<Plant>> GetAquatic()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Aquatic);
        }

        [HttpGet("biennial")]
        public async Task<IEnumerable<Plant>> GetBiennial()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Biennial);
        }

        [HttpGet("deciduous_shrub")]
        public async Task<IEnumerable<Plant>> GetDeciduousShrub()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is DeciduousShrub);
        }

        [HttpGet("deciduous_tree")]
        public async Task<IEnumerable<Plant>> GetDeciduousTree()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is DeciduousTree);
        }

        [HttpGet("evergreen_shrub")]
        public async Task<IEnumerable<Plant>> GetEvergreenShrub()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is EvergreenShrub);
        }

        [HttpGet("evergreen_tree")]
        public async Task<IEnumerable<Plant>> GetEvergreenTree()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is EvergreenTree);
        }

        [HttpGet("fern")]
        public async Task<IEnumerable<Plant>> GetFern()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Fern);
        }
            
        [HttpGet("grass")]
        public async Task<IEnumerable<Plant>> GetGrass()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Grass);
        }

        [HttpGet("mosses")]
        public async Task<IEnumerable<Plant>> GetMosses()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Mosses);
        }

        [HttpGet("perennial")]
        public async Task<IEnumerable<Plant>> GetPerennial()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Perennial);
        }

        [HttpGet("vine")]
        public async Task<IEnumerable<Plant>> GetVine()
        {
            return (await _plantService.GetAsync())
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Vine);
        }
    }
}
