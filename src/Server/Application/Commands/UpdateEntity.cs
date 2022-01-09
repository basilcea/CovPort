using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class UpdateEntityCommand<T> : IRequest<T> where T: IEntity
    {
        public UpdateEntityCommand(T entity){
            Entity = entity;
        }
        public T Entity {get;}
    }

    public class UpdateEntityCommandHandler<T> : IRequestHandler<UpdateEntityCommand<T>, T> where T : IEntity {

        private readonly IEntityRepository<T> _entityRepo;

        public UpdateEntityCommandHandler(IEntityRepository<T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(UpdateEntityCommand<T> request, CancellationToken cancellationToken)
        {
            var numOfEntries = await _entityRepo.Update(request.Entity, cancellationToken);
            return numOfEntries > 0 ? request.Entity : default;
         
        }
    }
}