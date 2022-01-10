using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;

namespace Infrastructure.Persistence
{
    public class DbConnection: IDbService
    {
    
        public async Task<PingResult> Ping()
        {
            return new PingResult("Database Unavailable");
        }
    }
}