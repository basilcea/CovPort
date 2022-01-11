// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Application.DTO;
// using Domain.Aggregates;
// using Domain.Entities;
// using Domain.Interfaces;
// using Domain.ValueObjects;
// using Infrastructure.Persistence;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructure.Repository
// {
//     public class ResultRepository<S,T> :  EntityRepository<S,T> , IEntityRepository<S,T>
//      where S : class  where T : class, IEntity

//     {
//         public ResultRepository(PortalDbContext dbContext) : base(dbContext)
//         {
//         }

//         public override async Task<IEnumerable<T>> GetByFilter(string filter = null)
//         {
//             var user = await DbContext.Users.FindAsync(filter);
//             if (user == null)
//             {
//                 throw new Exception();
//             }

//             if (user.UserRole == Role.USER.ToString())
//             {
//                 return DbContext.Results.Where(x => x.UserId == filter).ToListAsync() as IEnumerable<T>;
//             }

//             return await DbContext.Results.Where(x => x.Status == filter).ToListAsync() as IEnumerable<T>;
//         }

//         public override async Task<T> Update(S body)
//         {
//             var postBody = body as ResultPatchRequestBody;
//             var user = await DbContext.Users.FindAsync(postBody.RequesterId);
//             if (user == null)
//             {
//                 throw new Exception();
//             }

//             if (user.UserRole != Role.LABADMIN.ToString())
//             {
//                 throw new Exception();
//             }

//             var savedResult = await GetById(postBody.Id) as Result;
//             savedResult.Status = postBody.Status;
//             savedResult.Positive = Boolean.Parse(postBody.Positive);
//             return await UpdateEntity(savedResult as T, DbContext);
//         }

//         public override async Task<T> Insert(T entity, string requesterId)
//         {

//             var user = await DbContext.Users.FindAsync(requesterId);

//             if (user == null)
//             {
//                 throw new Exception();
//             }

//             if (user.UserRole != Role.LABADMIN.ToString())
//             {
//                 throw new Exception();
//             }
//            return await InsertEntity(entity, DbContext);
//         }
//     }
// }