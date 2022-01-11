// using System;
// using System.Threading.Tasks;
// using Application.DTO;
// using Domain.Entities;
// using Domain.Interfaces;
// using Domain.ValueObjects;
// using Infrastructure.Persistence;

// namespace Infrastructure.Repository
// {
//     public class SpaceRepository<S,T>:  EntityRepository<S, T>, IEntityRepository<S, T>
//      where S : class where T : class,IEntity

//     {         public SpaceRepository(PortalDbContext dbContext) : base(dbContext)
//         {
//         }

//          public override async Task<T> Insert(T entity, string requesterId)
//         {

//             var user = await DbContext.Users.FindAsync(requesterId);

//             if (user == null)
//             {
//                 throw new Exception();
//             }
//             if (user.UserRole != Role.ADMIN.ToString())
//             {
//                 throw new Exception();
//             }
//             return await InsertEntity(entity, DbContext);

//         }

//     }
// }