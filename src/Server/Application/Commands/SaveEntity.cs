using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class SaveEntity<T> : IRequest<T> where T: IEntity 
    {
        public T Body {get;set;}
        public int RequesterId {get;set;}
    }

    public class SaveEntityHandler<T> : IRequestHandler<SaveEntity<T>, T> where T : IEntity {

        private readonly IEntityRepository<T> _entityRepo;

        public SaveEntityHandler(IEntityRepository<T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(SaveEntity< T> request, CancellationToken cancellationToken)
        {
            return await _entityRepo.Insert(request.Body, request.RequesterId);
        
        }
    }
}