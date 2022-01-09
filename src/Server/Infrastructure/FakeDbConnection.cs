using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;

namespace Infrastructure
{
    public class FakeDbConnection: IDbService
    {
    
        public async Task<PingResult> Ping()
        {
            return new PingResult("Database Unavailable");
        }
    }
}