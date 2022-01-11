using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetEntity<T> : IRequest<IEnumerable<T>> where T : IEntity
    {
        public GetEntity(string filter)
        {
            Filter = filter;
        }
        public string Filter { get; }
    

    }
    public class GetEntityHandler<T> : IRequestHandler<GetEntity<T>, IEnumerable<T>> where T : IEntity 
    {
        private readonly IEntityRepository<string,T> _entityRepository;

        public GetEntityHandler(IEntityRepository<string, T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<IEnumerable<T>> Handle(GetEntity<T> request, CancellationToken cancellationToken)
        {
            if(request.Filter !=null)
            {
              return await _entityRepository.GetByFilter(request.Filter);
            }
            return await _entityRepository.Get();
        }
    }
}