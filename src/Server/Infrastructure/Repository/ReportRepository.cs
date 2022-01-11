using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repository
{
    public class ReportRepository: EntityRepository<ResultSummary, Result>
    {

        public ReportRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<ResultSummary>> GetSummary(string filter = null)
        {
            return null;
        }
    }
}