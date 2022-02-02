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
                result = await _dbContext.Bookings.Where(x => x.UserId == id).ToListAsync();
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
                    x.SpaceId == entity.SpaceId).ToListAsync();

                var space = await GetSpaceById(entity.SpaceId, _dbContext, _logger);
                _logger.LogInformation("Retrieved space: {@type}", space);

                if (space.SpacesCreated - existingBooking.Count < 1)
                {
                    space.Closed = true;
                    throw new NotFoundException("Available Space Not Found");
                }

                var userExistingBooking = existingBooking.AsEnumerable()
                .Where(x => x.UserId == entity.UserId && x.Status != BookingStatus.CANCELLED.ToString())
                .ToList();

                if (userExistingBooking.Count > 0)
                {
                    throw new BadRequestException("You have a pending booking for this date");
                }

                entity.Status = BookingStatus.PENDING.ToString();
                entity.LocationName = space.LocationName;
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
