using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BookingRepository : EntityRepository<BookingPatchRequestBody, Booking>
    {
        public BookingRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Booking> GetById(string Id)
        {
            var savedBooking = await DbContext.Bookings.FindAsync(Id);
            var spaces = await DbContext.Spaces.FindAsync(savedBooking.SpaceId);
            if (spaces.Date < DateTime.Now)
            {
                spaces.SpacesAvailable = 0;
                savedBooking.Status = BookingStatus.CLOSED.ToString();
                await DbContext.SaveChangesAsync();
            }
            return savedBooking;
        }
        public override async Task<IEnumerable<Booking>> GetByFilter(string filter)
        {
            return await DbContext.Bookings.Where(x => x.Status == filter).ToListAsync();
        }

        public override async Task<Booking> Update(BookingPatchRequestBody body)
        {
            var savedBooking = await GetById(body.Id);
            if (savedBooking.UserId != body.UserId)
            {
                throw new Exception();
            }
            if (savedBooking.Status != BookingStatus.PENDING.ToString())
            {
                throw new Exception();
            }
            var space = await DbContext.Spaces.FindAsync(savedBooking.SpaceId);
            space.SpacesAvailable += 1;
            savedBooking.Status = body.Status;
            return await UpdateEntity(savedBooking, DbContext);
        }
        public override async Task<Booking> Insert(Booking entity, string requesterId)
        {
            var user = await DbContext.Users.FindAsync(entity.UserId);
            if (user == null)
            {
                throw new Exception();
            }
            var space = await DbContext.Spaces.FindAsync(entity.SpaceId);
            if (space.SpacesAvailable <= 0)
            {
                throw new Exception();
            }
           return await InsertEntity(entity, DbContext);

        }
    }
}
