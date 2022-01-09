using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetEntityByFilters<T> : IRequest<T> where T : IEntity
    {
     public GetEntityByFilters(string email, string status)
     {
        Email = email;
        Status = status;
     }
    public string Email {get;}
    public string Status {get; set;}

    }
    public class GetEntityHandler<T> : IRequestHandler<GetEntityByFilters<T>, T> where T : IEntity
    {
        private readonly IEntityRepository<T> _entityRepository;

        public GetEntityHandler(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<T> Handle(GetEntityByFilters<T> request, CancellationToken cancellationToken)
        {
            return await _entityRepository.Get(request.Email, request.Status, cancellationToken);
        }
    }
}