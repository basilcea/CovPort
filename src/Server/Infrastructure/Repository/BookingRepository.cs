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
    public class BookingRepository : EntityRepository<Booking>
    {

        public BookingRepository(PortalDbContext dbContext, ILogger<Booking> logger) : base(dbContext, logger)
        {
        }

        public override async Task<Booking> GetById(int Id)
        {
            try
            {
                var savedBooking = await _dbContext.Bookings.FindAsync(Id);
                if (savedBooking == null)
                {
                    throw new NotFoundException("Booking Not Found");
                }
                var space = await GetSpaceById(savedBooking.SpaceId, _dbContext,_logger);
                _logger.LogInformation("Received booking response: {@response}", savedBooking);
                return savedBooking;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
        public override async Task<IEnumerable<Booking>> GetByFilter(string filter)
        {
            try
            {
                int id;
                var value = int.TryParse(filter, out id);
                IEnumerable<Booking> result;
                if (!value)
                {
                    result = await _dbContext.Bookings.Where(x => x.Status == filter.ToUpper()).ToListAsync();
                    _logger.LogInformation("Received booking response: {@response}", result);
                    return result;
                }
                result = await _dbContext.Bookings.Where(x => x.UserId == id && x.Status == BookingStatus.PENDING.ToString()).ToListAsync();
                _logger.LogInformation("Received booking response: {@response}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();
            }
        }

        public override async Task<Booking> Update(Booking body, int requesterId)
        {
            try
            {
                var savedBooking = await GetById(body.Id);
                if (savedBooking.Status != BookingStatus.PENDING.ToString())
                {
                    throw new BadRequestException("No Pending Booking Exists");
                }
                if (savedBooking.UserId != body.UserId)
                {
                    throw new UnauthorizedException();
                }

                var space = await _dbContext.Spaces.FindAsync(savedBooking.SpaceId);
                space.SpacesAvailable += 1;
                _logger.LogInformation($"Updated space available in space {space.Id} to {space.SpacesAvailable}");
                savedBooking.Status = body.Status ?? savedBooking.Status;
                return await UpdateEntity(savedBooking, _dbContext, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();
            }
        }
        public override async Task<Booking> Insert(Booking entity, int requesterId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(entity.UserId);
                if (user == null)
                {
                    throw new NotFoundException("User Not Found");
                }

                var existingBooking = await _dbContext.Bookings.Where(x =>
                    x.SpaceId == entity.SpaceId &&
                    x.UserId == entity.UserId &&
                    x.Status != BookingStatus.CANCELLED.ToString())
                    .FirstOrDefaultAsync();

                if (existingBooking != null)
                {
                    throw new BadRequestException("Pending Booking Exists");
                }

                var space = await GetSpaceById(entity.SpaceId, _dbContext,_logger);

                if (space.SpacesAvailable <= 0)
                {
                    throw new NotFoundException("Available Space Not Found");
                }
                space.SpacesAvailable -= 1;
                _logger.LogInformation($"Updated space available in space {space.Id} to {space.SpacesAvailable}");
                entity.Status = BookingStatus.PENDING.ToString();
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
