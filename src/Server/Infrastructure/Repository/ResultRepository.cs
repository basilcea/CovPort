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
    public class ResultRepository : EntityRepository<Result>
    {
        public ResultRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Result>> GetByFilter(string filter)
        {
            int id;
            var value = int.TryParse(filter, out id);

            if (!value)
            {
                return await DbContext.Results.
                Where(x => x.Status == filter.ToUpper())
                .ToListAsync();
            }

            return await DbContext.Results
            .Where(x => x.UserId == int.Parse(filter)
            && x.Status == TestStatus.COMPLETED.
            ToString()).ToListAsync();

        }

        public override async Task<Result> Update(Result body, int requesterId)
        {
            var user = await DbContext.Users.FindAsync(requesterId);
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
            return await UpdateEntity(savedResult, DbContext);
        }

        public override async Task<Result> Insert(Result entity, int requesterId)
        {
            var user = await DbContext.Users.FindAsync(requesterId);

            if (user == null)
            {
                throw new NotFoundException("User Not Found");
            }

            if (user.UserRole != Role.LABADMIN.ToString())
            {
                throw new UnauthorizedException();

            }
            var booking = await DbContext.Bookings
            .FindAsync(entity.BookingId);
            if (booking == null || booking.Status != BookingStatus.PENDING.ToString())
            {
                throw new NotFoundException("Booking Not Found");
            }
            var spaces = await DbContext.Spaces.FindAsync(booking.SpaceId);

            if (entity.TestLocation.ToUpper() != spaces.LocationName.ToUpper())
            {
                throw new BadRequestException("TestLocation - BookingLocation Mismatch");

            }

            booking.Status = BookingStatus.CLOSED.ToString();
            return await InsertEntity(entity, DbContext);
        }
    }
}