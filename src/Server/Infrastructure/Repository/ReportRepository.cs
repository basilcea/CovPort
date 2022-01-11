// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Domain.Aggregates;
// using Domain.Entities;
// using Domain.Interfaces;
// using Infrastructure.Persistence;

// namespace Infrastructure.Repository
// {
//     public class ReportRepository<S,T>: EntityRepository<S,T> , IEntityRepository<S,T>
//      where S : class  where T : class, IEntity
//     {

//         public ReportRepository(PortalDbContext dbContext) : base(dbContext)
//         {
//         }
//         public override async Task<IEnumerable<S>> GetSummary(string filter = null)
//         {
//             return null;
//         // }
//     }
// }