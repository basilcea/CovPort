using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : EntityRepository<UserSummary, User>
    {
        public UserRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<User>> GetByFilter(string filter)
        {
            return await DbContext.Users.Where(x => x.Email == filter).ToListAsync();
        }

        public override async Task<IEnumerable<UserSummary>> GetSummary(string userId)
        {
            var user = await DbContext.Users.FindAsync(userId);

            var pendingBooking = await DbContext.Bookings.Where(
                x => x.UserId == userId &&
                x.Status == BookingStatus.PENDING.ToString()
                ).ToListAsync();

            var completedTests = await DbContext.Results.Where(
                x => x.UserId == userId &&
                x.Status == TestStatus.COMPLETED.ToString()
                ).ToListAsync();

            var userList = new[] {
                new UserSummary{
                    User = user,
                    Results = completedTests,
                    Bookings = pendingBooking
                }
            };
         return userList;

        }
    }
}