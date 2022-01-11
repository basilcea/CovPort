using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;

namespace Infrastructure.Persistence
{
    public class DbConnection: IDbService
    {
      private readonly PortalDbContext _dbContext;

        public DbConnection(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PingResult> Ping()
        {
           var result = await _dbContext.Database.CanConnectAsync();
            if (result)
            {
                return new PingResult(result);
            }

            return new PingResult("Database Unavailable");
        }
    }
}