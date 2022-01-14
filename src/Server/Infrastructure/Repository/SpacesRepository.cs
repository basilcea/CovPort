using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class SpaceRepository : EntityRepository<Space>
    {
        public SpaceRepository(PortalDbContext dbContext, ILogger<Space> logger) : base(dbContext, logger)
        {
        }

        public override async Task<Space> Insert(Space entity, int requesterId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(requesterId);

                if (user == null)
                {
                    throw new NotFoundException("User Not Found");
                }
                if (user.UserRole != Role.ADMIN.ToString())
                {
                    throw new UnauthorizedException();
                }
                if (entity.Date < DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    throw new BadRequestException("Space Closed");
                }
                return await InsertEntity(entity, _dbContext, _logger);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }


        }

        public override async Task<Space> GetById(int id)
        {
            try
            {
                var space = await GetSpaceById(id, _dbContext,_logger);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Retrieved space: {@type}", space);
                return space;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }

        }

        public override async Task<IEnumerable<Space>> GetByFilter(string location)
        {
            try
            {
                var space = await _dbContext.Spaces.Where(x=> x.LocationName.ToUpper() == location.ToUpper() && x.SpacesAvailable > 0).ToListAsync();
                _logger.LogInformation("Retrieved space: {@type}", space);
                return space;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }

        }


    }
}