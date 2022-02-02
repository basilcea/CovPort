using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class UserRepository : EntityRepository<User>
    {
        public UserRepository(PortalDbContext dbContext, ILogger<User> logger) : base(dbContext, logger)
        {
        }
        public override async Task<IEnumerable<User>> GetByFilter(string filter)
        {
            try
            {
                var result = await _dbContext.Users.Where(x => x.Email == filter).ToListAsync();
                if (result.Count <= 0)
                {
                    throw new NotFoundException("User Not Found");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
    }
}