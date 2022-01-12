using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task<T> Insert(T body, int requesterId);
        Task<T> Update(T body, int requesterId);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> Get(); 
        Task<IEnumerable<T>> GetByFilter( string filter);
        
    }
}