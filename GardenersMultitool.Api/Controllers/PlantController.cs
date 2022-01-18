using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IDictionary<int, Plant> _ourPlants;

        public PlantController(ILogger<PlantController> logger, Dictionary<int, Plant> ourPlants)
        {
            _logger = logger;
            _ourPlants = ourPlants;
        }

        [HttpGet]
        public Plant GetById(int id)
        {
            _ourPlants.TryGetValue(id, out var plant);
            return plant;
        }

        [HttpGet("annuals")]
        public IEnumerable<Plant> GetAnnuals()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Annual);
        }

        [HttpGet("aquatic")]
        public IEnumerable<Plant> GetAquatic()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Aquatic);
        }

        [HttpGet("Biennial")]
        public IEnumerable<Plant> GetBiennial()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Biennial);
        }

        [HttpGet("deciduous_shrub")]
        public IEnumerable<Plant> GetDeciduousShrub()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is DeciduousShrub);
        }

        [HttpGet("deciduous_tree")]
        public IEnumerable<Plant> GetDeciduousTree()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is DeciduousTree);
        }
    }
}

