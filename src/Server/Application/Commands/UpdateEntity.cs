using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class UpdateEntity<T> : IRequest<T>  where T: IEntity 
    {
        public T Body {get;set;}
        public int RequesterId{get;set;}
       
    }

    public class UpdateEntityHandler<T> : IRequestHandler<UpdateEntity<T>, T>   where T : IEntity {

        private readonly IEntityRepository<T> _entityRepo;

        public UpdateEntityHandler(IEntityRepository<T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(UpdateEntity<T> request, CancellationToken cancellationToken)
        {
           return await _entityRepo.Update(request.Body, request.RequesterId);
        }
    }
}