using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class SaveEntity<S,T> : IRequest<T> where T: IEntity where S:class
    {
        public SaveEntity(T body, string userId){
            Body = body;
            RequesterId = userId;
        }
        public T Body {get;}
        public string RequesterId {get;}
    }

    public class SaveEntityHandler<S,T> : IRequestHandler<SaveEntity<S, T>, T> where T : IEntity where S:class {

        private readonly IEntityRepository<S,T> _entityRepo;

        public SaveEntityHandler(IEntityRepository<S,T> entityRepo)
        {
            _entityRepo = entityRepo;
        }
        public async Task<T> Handle(SaveEntity<S, T> request, CancellationToken cancellationToken)
        {
            return await _entityRepo.Insert(request.Body, request.RequesterId);
        
        }
    }
}