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
                return await _dbContext.Users.Where(x => x.Email == filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
    }
}