﻿using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Plants
{
    public class GetPlantByPlantId : IRequest<Plant>
    {
        public int PlantId { get; }

        public GetPlantByPlantId(int id)
        {
            PlantId = id;
        }
    }

    public class GetRequestByPlantIdHandler : RequestHandler<GetPlantByPlantId, Plant>
    {
        public GetRequestByPlantIdHandler(DataContext context) : base(context)
        {
        }

        public override async Task<Plant> Handle(GetPlantByPlantId request, CancellationToken cancellationToken) =>
            await Context.Plants
                .Find(plant => plant.PlantId == request.PlantId)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
