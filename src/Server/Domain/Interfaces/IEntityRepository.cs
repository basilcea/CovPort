using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntityRepository<T> where T: IEntity
    {
      Task<int> Insert(T entity, CancellationToken cancellationToken);
      Task<int> Update(T entity, CancellationToken cancellationToken);
      Task<T> GetById(string id, CancellationToken cancellationToken);
      Task<T> Get(CancellationToken cancellationToken); 
    }
}