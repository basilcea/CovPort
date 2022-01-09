using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Infrastructure
{
    public class FakeEntityRepository<T> : IEntityRepository<T> where T: IEntity
    {
        public Task<System.Collections.Generic.IEnumerable<T>> Get(string email, string status, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<T> GetById(string id, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<int> Insert(T entity, CancellationToken cancellationToken)
        {
            return 1;
        }
        public async Task<int> Update(T entity, CancellationToken cancellationToken)
        {
            return 1;
        }
    }
}