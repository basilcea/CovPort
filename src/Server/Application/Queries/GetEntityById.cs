using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetEntityById<T> : IRequest<T> where T : IEntity
    {
        public GetEntityById(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
    public class GetEntityByIdHandler<T> : IRequestHandler<GetEntityById<T>, T> where T : IEntity 
    {
        private readonly IEntityRepository<string, T> _entityRepository;

        public GetEntityByIdHandler(IEntityRepository<string, T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<T> Handle(GetEntityById<T> request, CancellationToken cancellationToken)
        {
            return await _entityRepository.GetById(request.Id);
        }
    }
}