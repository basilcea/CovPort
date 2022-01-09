using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IEntityRepository<T> where T: IEntity
    {
      Task<int> Insert(T entity, CancellationToken cancellationToken);
      Task<int> Update(T entity, CancellationToken cancellationToken);
      Task<T> GetById(string id, CancellationToken cancellationToken);
      Task<IEnumerable<T>> Get(string email, string status, CancellationToken cancellationToken); 
    }
}