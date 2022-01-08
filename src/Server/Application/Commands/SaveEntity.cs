using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class SaveEntityCommand<T> : IRequest<T> where T: IEntity
    {
        public SaveEntityCommand(T entity){
            Entity = entity;
        }
        public T Entity {get;}
    }

    public class SaveEntityCommandHandler<T> : IRequestHandler<SaveEntityCommand<T>, T> where T : IEntity {

        private readonly IEntityRepository<T> _entityRepo;

        public SaveEntityCommandHandler(IEntityRepository<T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(SaveEntityCommand<T> request, CancellationToken cancellationToken)
        {
            var numOfEntries = await _entityRepo.Insert(request.Entity, cancellationToken);
            return numOfEntries > 0 ? request.Entity : default;
         
        }
    }
}