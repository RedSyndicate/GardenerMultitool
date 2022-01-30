using System.Collections.Generic;
using System.Linq;
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
        public int? Total { get; set; }
        public int? Page { get; set; }
        public int? PerPage { get; set; }

        public GetAllPlants()
        {
            Total = null;
            Page = null;
            PerPage = null;
        }

        public GetAllPlants(int total, int page, int perPage)
        {
            Total = total;
            Page = page;
            PerPage = perPage;
        }
    }
    public class GetAllRequestsHandler : RequestHandler<GetAllPlants, IEnumerable<Plant>>
    {
        public GetAllRequestsHandler(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Plant>> Handle(GetAllPlants request, CancellationToken cancellationToken) =>
            await Context.Plants.Find(plant => true).Skip(request.Page ?? 0 * request.PerPage ?? 0).Limit(request.Total).ToListAsync(cancellationToken);

    }
}
