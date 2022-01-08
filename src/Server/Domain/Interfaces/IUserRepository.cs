using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
         Task<User> GetUserByEmail(string email, CancellationToken token);
         Task<User> GetUserById (string Id);
    }
}