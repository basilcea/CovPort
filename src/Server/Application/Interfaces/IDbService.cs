using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IDbService
    {
         Task<PingResult> Ping();
    }
}