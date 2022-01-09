using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetSummary<S, T> : IRequest<T> where S : IEntity where T:class
    {
        
        public GetSummary(string Id) {

        }
    }

    public class GetSummaryHandler<S, T> : IRequestHandler<GetSummary<S, T>, T> where S : IEntity where T:class
    {
        public Task<T> Handle(Queries.GetSummary<S, T> request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}