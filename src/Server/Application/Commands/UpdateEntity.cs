using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class UpdateEntity<S, T> : IRequest<T> where S:class where T: IEntity 
    {
        public UpdateEntity(S body){
            Body = body;
        }
        public S Body {get;}
       
    }

    public class UpdateEntityHandler<S,T> : IRequestHandler<UpdateEntity<S,T>, T>  where S: class where T : IEntity {

        private readonly IEntityRepository<S,T> _entityRepo;

        public UpdateEntityHandler(IEntityRepository<S,T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(UpdateEntity<S,T> request, CancellationToken cancellationToken)
        {
           return await _entityRepo.Update(request.Body);
        }
    }
}