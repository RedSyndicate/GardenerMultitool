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

        [HttpGet("biennial")]
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

        [HttpGet("evergreen_shrub")]
        public IEnumerable<Plant> GetEvergreenShrub()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is EvergreenShrub);
        }

        [HttpGet("evergreen_tree")]
        public IEnumerable<Plant> GetEvergreenTree()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is EvergreenTree);
        }

        [HttpGet("fern")]
        public IEnumerable<Plant> GetFern()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Fern);
        }
            
        [HttpGet("grass")]
        public IEnumerable<Plant> GetGrass()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Grass);
        }

        [HttpGet("mosses")]
        public IEnumerable<Plant> GetMosses()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Mosses);
        }

        [HttpGet("perennial")]
        public IEnumerable<Plant> GetPerennial()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Perennial);
        }

        [HttpGet("vine")]
        public IEnumerable<Plant> GetVines()
        {
            return _ourPlants.Values
                .Where(plant => plant.SoilPH != Maybe<pH>.None)
                .Where(plant => plant.PlantType is Vine);
        }
    }
}
