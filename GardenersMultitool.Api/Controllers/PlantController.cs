using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.PlantType;
using MediatR;
using GardenersMultitool.Api.UseCases.Plants;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Helpers;

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

        [HttpGet("{plantId:int}")]
        public async Task<Plant> GetPlantById(int plantId) => await _mediator.Send(new GetPlantById(plantId));

        [HttpGet]
        public async Task<IEnumerable<Plant>> GetPlants() => await _mediator.Send(new GetAllPlants());

        [HttpGet("{plantType}")]
        public async Task<IEnumerable<Plant>> GetPlantsByPlantType(string plantType) => await _mediator.Send(new GetPlantsByPlantType(plantType));
        //[HttpGet("annuals")]
        //public async Task<IEnumerable<Plant>> GetAnnuals() => await _mediator.Send(new GetPlant)
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Annual);
        //}

        //[HttpGet("aquatic")]
        //public async Task<IEnumerable<Plant>> GetAquatic()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Aquatic);
        //}

        //[HttpGet("biennial")]
        //public async Task<IEnumerable<Plant>> GetBiennial()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Biennial);
        //}

        //[HttpGet("deciduous_shrub")]
        //public async Task<IEnumerable<Plant>> GetDeciduousShrub()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is DeciduousShrub);
        //}

        //[HttpGet("deciduous_tree")]
        //public async Task<IEnumerable<Plant>> GetDeciduousTree()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is DeciduousTree);
        //}

        //[HttpGet("evergreen_shrub")]
        //public async Task<IEnumerable<Plant>> GetEvergreenShrub()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is EvergreenShrub);
        //}

        //[HttpGet("evergreen_tree")]
        //public async Task<IEnumerable<Plant>> GetEvergreenTree()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is EvergreenTree);
        //}

        //[HttpGet("fern")]
        //public async Task<IEnumerable<Plant>> GetFern()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Fern);
        //}

        //[HttpGet("grass")]
        //public async Task<IEnumerable<Plant>> GetGrass()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Grass);
        //}

        //[HttpGet("mosses")]
        //public async Task<IEnumerable<Plant>> GetMosses()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Mosses);
        //}

        //[HttpGet("perennial")]
        //public async Task<IEnumerable<Plant>> GetPerennial()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Perennial);
        //}

        //[HttpGet("vine")]
        //public async Task<IEnumerable<Plant>> GetVine()
        //{
        //    return (await _plantService.GetAsync())
        //        .Where(plant => plant.SoilPH != Maybe<pH>.None)
        //        .Where(plant => plant.PlantType is Vine);
        //}
    }
}
