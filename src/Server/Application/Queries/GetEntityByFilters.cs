using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetEntity<T> : IRequest<T> where T : IEntity
    {
     public GetEntity(string email=null, string status=null)
     {
        Email = email;
        Status = status;
     }
    public string Email {get;}
    public string Status {get; set;}

    }
    public class GetEntityHandler<T> : IRequestHandler<GetEntity<T>, T> where T : IEntity
    {
        private readonly IEntityRepository<T> _entityRepository;

        public GetEntityHandler(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<T> Handle(GetEntity<T> request, CancellationToken cancellationToken)
        {
            return await _entityRepository.Get(request.Email, request.Status, cancellationToken);
        }
    }
}