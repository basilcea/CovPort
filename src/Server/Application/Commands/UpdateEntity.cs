using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class UpdateEntity<T> : IRequest<T> where T: IEntity
    {
        public UpdateEntity(T entity){
            Entity = entity;
        }
        public T Entity {get;}
    }

    public class UpdateEntityHandler<T> : IRequestHandler<UpdateEntity<T>, T> where T : IEntity {

        private readonly IEntityRepository<T> _entityRepo;

        public UpdateEntityHandler(IEntityRepository<T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(UpdateEntity<T> request, CancellationToken cancellationToken)
        {
            var numOfEntries = await _entityRepo.Update(request.Entity, cancellationToken);
            return numOfEntries > 0 ? request.Entity : default;
        }
    }
}