using System.Threading;
using System.Threading.Tasks;
using GardenersMultitool.Api.UseCases.Context;
using MediatR;

namespace GardenersMultitool.Api.UseCases.Locations
{
    public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        //protected readonly IMongoCollection<Location> Context;
        protected readonly DataContext Context;

        protected RequestHandler(DataContext context)
        {
            Context = context;
        }

        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }


}

