using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetSummary<S, T> : IRequest<IEnumerable<S>> where S : class where T : IEntity
    {

        public GetSummary(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }

    public class GetSummaryHandler<S, T> : IRequestHandler<GetSummary<S, T>, IEnumerable<S>> where S : class where T : IEntity
    {
        private readonly IEntityRepository<S,T> _entityRepository;

        public GetSummaryHandler(IEntityRepository<S,T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<IEnumerable<S>> Handle(Queries.GetSummary<S, T> request, CancellationToken cancellationToken)
        {
            return await _entityRepository.GetSummary(request.Id);
        }
    }
}