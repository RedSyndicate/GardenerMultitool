using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace GardenersMultitool.Api.UseCases.Plants
{
    public class GetPlantsByFilter : IRequest<IEnumerable<Plant>>
    {
        public PlantByFilter PlantFilter { get; set; }
        public Pagination Pagination { get; set; }


        public GetPlantsByFilter(PlantByFilter plantByFilter, Pagination pagination)
        {
            PlantFilter = plantByFilter;
            Pagination = pagination;
        }
    }

    public class GetPlantsByFilterHandler : RequestHandler<GetPlantsByFilter, IEnumerable<Plant>>
    {
        public GetPlantsByFilterHandler(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Plant>> Handle(GetPlantsByFilter request,
            CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<Plant>.Filter;

            var filter = filterBuilder
                             .Where(plant => plant.Name.Value.Contains(request.PlantFilter.Name.Value))
                         | filterBuilder
                             .Where(plant => plant.ScientificName.Value.Contains(request.PlantFilter.ScientificName.Value))
                         | filterBuilder
                             .Where(plant => plant.PlantId == request.PlantFilter.PlantId);

            return await Context.Plants.Find(filter)
                .SortBy(p => p.Name)
                .Limit(request.Pagination.Limit)
                .Skip(request.Pagination.Offset)
                .ToListAsync(cancellationToken);
        }
    }
}