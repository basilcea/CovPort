using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class SaveEntity<T> : IRequest<T> where T: IEntity
    {
        public SaveEntity(T entity){
            Entity = entity;
        }
        public T Entity {get;}
    }

    public class SaveEntityHandler<T> : IRequestHandler<SaveEntity<T>, T> where T : IEntity {

        private readonly IEntityRepository<T> _entityRepo;

        public SaveEntityHandler(IEntityRepository<T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(SaveEntity<T> request, CancellationToken cancellationToken)
        {
            var numOfEntries = await _entityRepo.Insert(request.Entity, cancellationToken);
            return numOfEntries > 0 ? request.Entity : default;
         
        }
    }
}