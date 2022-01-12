using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class SpaceRepository : EntityRepository<Space>
    {
        public SpaceRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Space> Insert(Space entity, int requesterId)
        {

            var user = await DbContext.Users.FindAsync(requesterId);

            if (user == null)
            {
                throw new NotFoundException("User Not Found");
            }
            if (user.UserRole != Role.ADMIN.ToString())
            {
                throw new UnauthorizedException();
            }
            if(entity.Date < DateTime.Parse(DateTime.Now.ToShortDateString())){
                throw new BadRequestException("Space Closed");
            }
            return await InsertEntity(entity, DbContext);

        }

        public override async Task<Space> GetById(int Id)
        {
            var space = await DbContext.Spaces.FindAsync(Id);
            if (space == null){
                throw new NotFoundException("Space Not Found");
            }
            if (space.Date < DateTime.Parse(DateTime.Now.ToShortDateString()))
            {
                space.SpacesAvailable = 0;
                var bookings = await DbContext.Bookings.Where(x => x.SpaceId == Id && x.Status != BookingStatus.CLOSED.ToString()).ToListAsync();
                foreach (var booking in bookings)
                {
                    booking.Status = BookingStatus.CLOSED.ToString();
                }
            }
            await DbContext.SaveChangesAsync();
            return space;
        }

    }
}