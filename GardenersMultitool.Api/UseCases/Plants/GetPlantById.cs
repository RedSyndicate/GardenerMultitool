using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Plants
{
    public class GetPlantById : IRequest<Plant>
    {
        public int PlantId { get; }

        public GetPlantById(int id)
        {
            PlantId = id;
        }
    }

    public class GetRequestByIdHandler : RequestHandler<GetPlantById, Plant>
    {
        public GetRequestByIdHandler(DataContext context) : base(context)
        {
        }

        public override async Task<Plant> Handle(GetPlantById request, CancellationToken cancellationToken) =>
            await Context.Plants
                .Find(plant => plant.PlantId == request.PlantId)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
