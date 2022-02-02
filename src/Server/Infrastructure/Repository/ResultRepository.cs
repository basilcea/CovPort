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
    public class ResultRepository : EntityRepository<Result>
    {
        public ResultRepository(PortalDbContext dbContext, ILogger<Result> logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<Result>> GetByFilter(string filter)
        {
            try
            {
                int id;
                var value = int.TryParse(filter, out id);
                IEnumerable<Result> result;

                if (!value)
                {
                    result = await _dbContext.Results.
                    Where(x => x.Status == filter.ToUpper())
                    .ToListAsync();
                    _logger.LogInformation("Received Result response: {@response}", result);
                    return result;
                }

                result = await _dbContext.Results
                .Where(x => x.UserId == int.Parse(filter)).ToListAsync();
                _logger.LogInformation("Received Result response: {@response}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }


        }

        public override async Task<Result> Update(Result body, int requesterId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(requesterId);
                if (user == null)
                {
                    throw new NotFoundException("User Not Found");
                }

                if (user.UserRole != Role.LABADMIN.ToString())
                {
                    throw new UnauthorizedException();
                }

                var savedResult = await GetById(body.Id);
                savedResult.Status = body.Status;
                savedResult.Positive = body.Positive;
                return await UpdateEntity(savedResult, _dbContext, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }

        }

        public override async Task<Result> Insert(Result entity, int requesterId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(requesterId);

                if (user == null)
                {
                    throw new NotFoundException("User Not Found");
                }

                if (user.UserRole != Role.LABADMIN.ToString())
                {
                    throw new UnauthorizedException();
                }
                var booking = await _dbContext.Bookings.FindAsync(entity.BookingId);

                if (booking == null || booking.Status != BookingStatus.PENDING.ToString())
                {
                    throw new NotFoundException("No Pending Booking");
                }
                var spaces = await _dbContext.Spaces.FindAsync(booking.SpaceId);

                if (entity.TestLocation.ToUpper() != spaces.LocationName.ToUpper())
                {
                    throw new BadRequestException("TestLocation - BookingLocation Mismatch");

                }

                booking.Status = BookingStatus.CLOSED.ToString();
                return await InsertEntity(entity, _dbContext, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }

        }
    }
}