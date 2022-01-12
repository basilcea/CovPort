using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : EntityRepository<User>
    {
        public UserRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<User>> GetByFilter(string filter)
        {
            return await DbContext.Users.Where(x => x.Email == filter).ToListAsync();
        }
    }
}