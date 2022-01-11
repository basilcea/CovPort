using System;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;

namespace Infrastructure.Repository
{
    public class SpaceRepository : EntityRepository<SpaceRequestBody, Space>
    {
        public SpaceRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

         public override async Task<Space> Insert(Space entity, string requesterId)
        {

            var user = await DbContext.Users.FindAsync(requesterId);

            if (user == null)
            {
                throw new Exception();
            }
            if (user.UserRole != Role.ADMIN.ToString())
            {
                throw new Exception();
            }
            return await InsertEntity(entity, DbContext);

        }

    }
}