using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Plants
{
    public class GetAllPlants : IRequest<IEnumerable<Plant>>
    {
        public int Total { get; set; }

        public GetAllPlants(int total)
        {
            Total = total;
        }
    }
    public class GetAllRequestsHandler : RequestHandler<GetAllPlants, IEnumerable<Plant>>
    {
        public GetAllRequestsHandler(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Plant>> Handle(GetAllPlants request, CancellationToken cancellationToken) =>
            await Context.Plants.Find(plant => true).ToListAsync(cancellationToken);

    }
}
