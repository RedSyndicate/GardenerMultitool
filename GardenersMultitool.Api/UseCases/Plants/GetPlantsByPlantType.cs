using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.Extensions;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.PlantType;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Plants
{
    public class GetPlantsByPlantType : IRequest<List<Plant>>
    {
        public string PlantType { get; }


        public GetPlantsByPlantType(string plantType)
        {
            PlantType = plantType;
        }
    }

    public class GetPlantsByPlantTypeHandler : RequestHandler<GetPlantsByPlantType, List<Plant>>
    {
        public GetPlantsByPlantTypeHandler(DataContext context) : base(context)
        {
        }

        public override async Task<List<Plant>> Handle(GetPlantsByPlantType request, CancellationToken cancellationToken)
        {
            var results = await Context.Plants
                .Find(plant => plant.PlantType == request.PlantType.ToPlantType())
                .ToListAsync(cancellationToken);

            return results;
        }
    }
}
