using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BookingRepository : EntityRepository<Booking>
    {
        public BookingRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Booking> GetById(int Id)
        {
                var savedBooking = await DbContext.Bookings.FindAsync(Id);
                if(savedBooking == null){
                    throw new NotFoundException("Booking Not Found");
                }
                await new SpaceRepository(DbContext).GetById(savedBooking.SpaceId);
                return savedBooking;
        }
        public override async Task<IEnumerable<Booking>> GetByFilter(string filter)
        {
            int id;
            var value = int.TryParse(filter, out id);

            if (!value)
            {
                return await DbContext.Bookings.Where(x => x.Status == filter.ToUpper()).ToListAsync();
            }
            return await DbContext.Bookings.Where(x => x.UserId == id && x.Status == BookingStatus.PENDING.ToString()).ToListAsync();
        }

        public override async Task<Booking> Update(Booking body, int requesterId)
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

            var space = await DbContext.Spaces.FindAsync(savedBooking.SpaceId);
            space.SpacesAvailable += 1;
            savedBooking.Status = body.Status ?? savedBooking.Status;
            return await UpdateEntity(savedBooking, DbContext);
        }
        public override async Task<Booking> Insert(Booking entity, int requesterId)
        {
            var user = await DbContext.Users.FindAsync(entity.UserId);
            if (user == null)
            {
                  throw new NotFoundException("User Not Found");
            }

            var existingBooking = await DbContext.Bookings.Where(x =>
                x.SpaceId == entity.SpaceId &&
                x.UserId == entity.UserId &&
                x.Status != BookingStatus.CANCELLED.ToString())
                .FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                throw new BadRequestException("Pending Booking Exists");
                
            }

            var space = await new SpaceRepository(DbContext)
            .GetById(entity.SpaceId); ;

            if (space.SpacesAvailable <= 0)
            {
                throw new NotFoundException("Available Space Not Found");
            }
            space.SpacesAvailable -= 1;
            entity.Status = BookingStatus.PENDING.ToString();
            return await InsertEntity(entity, DbContext);

        }
    }
}
