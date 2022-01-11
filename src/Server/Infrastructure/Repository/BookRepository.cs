// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Application.DTO;
// using Domain.Entities;
// using Domain.Interfaces;
// using Domain.ValueObjects;
// using Infrastructure.Persistence;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructure.Repository
// {
//     public class BookingRepository<S,T> : EntityRepository<S,T> , IEntityRepository<S,T>
//      where S : class  where T : class, IEntity
//     {
//         public BookingRepository(PortalDbContext dbContext) : base(dbContext)
//         { 
//         }

//         public override async Task<T> GetById(string Id)
//         {
//             var savedBooking = await DbContext.Bookings.FindAsync(Id);
//             var spaces = await DbContext.Spaces.FindAsync(savedBooking.SpaceId);
//             if (spaces.Date < DateTime.Now)
//             {
//                 spaces.SpacesAvailable = 0;
//                 savedBooking.Status = BookingStatus.CLOSED.ToString();
//                 await DbContext.SaveChangesAsync();
//             }
//             return savedBooking as T;
//         }
//         public override async Task<IEnumerable<T>> GetByFilter(string filter)
//         {
//             var result = await DbContext.Bookings.Where(x => x.Status == filter).ToListAsync() ;
//             return result as IEnumerable<T>;
//         }

//         public override async Task<T> Update(S body)
//         {
//             var postBody = body as BookingPatchRequestBody;
//             var savedBooking = await GetById(postBody.Id) as Booking;
//             if (savedBooking.UserId != postBody.UserId)
//             {
//                 throw new Exception();
//             }
//             if (savedBooking.Status != BookingStatus.PENDING.ToString())
//             {
//                 throw new Exception();
//             }
//             var space = await DbContext.Spaces.FindAsync(savedBooking.SpaceId);
//             space.SpacesAvailable += 1;
//             savedBooking.Status = postBody.Status;
//             return await UpdateEntity(savedBooking as T, DbContext);
//         }
//         public override async Task<T> Insert(T entity, string requesterId)
//         {
//             var booking = entity as Booking;
//             var user = await DbContext.Users.FindAsync(booking.UserId);
//             if (user == null)
//             {
//                 throw new Exception();
//             }
//             var space = await DbContext.Spaces.FindAsync(booking.SpaceId);
//             if (space.SpacesAvailable <= 0)
//             {
//                 throw new Exception();
//             }
//            return await InsertEntity(booking as T, DbContext);

//         }
//     }
// }
