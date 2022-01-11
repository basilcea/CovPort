using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntityRepository<S, T> where S : class where T : IEntity
    {
        Task<T> Insert(T body, string requesterId);
        Task<T> Update(S body);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> Get(); 
        Task<IEnumerable<T>> GetByFilter( string filter);
        Task<IEnumerable<S>> GetSummary(string filter);
    }
}