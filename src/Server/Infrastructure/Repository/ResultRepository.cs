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
    public class ResultRepository : EntityRepository<ResultPatchRequestBody, Result>
    {

        public ResultRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Result>> GetByFilter(string filter = null, string requesterId=null)
        {
            var user = await DbContext.Users.FindAsync(requesterId);
            if (user == null)
            {
                throw new Exception();
            }

            if (user.UserRole == Role.USER.ToString())
            {
                return DbContext.Results.Where(x => x.UserId == requesterId && x.Status == TestStatus.COMPLETED.ToString());
            }

            return await DbContext.Results.Where(x => x.Status == filter).ToListAsync();
        }

        public override async Task<Result> Update(ResultPatchRequestBody body, string requesterId)
        {
            var user = await DbContext.Users.FindAsync(requesterId);
            if (user == null)
            {
                throw new Exception();
            }

            if (user.UserRole != Role.LABADMIN.ToString())
            {
                throw new Exception();
            }

            var savedResult = await GetById(body.Id);
            savedResult.Status = body.Status;
            savedResult.Positive = Boolean.Parse(body.Positive);
            return await UpdateEntity(savedResult, DbContext);
        }

        public override async Task<Result> Insert(Result entity, string requesterId)
        {

            var user = await DbContext.Users.FindAsync(requesterId);

            if (user == null)
            {
                throw new Exception();
            }

            if (user.UserRole != Role.LABADMIN.ToString())
            {
                throw new Exception();
            }
           return await InsertEntity(entity, DbContext);
        }
    }
}