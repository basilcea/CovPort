using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Aggregates;

namespace Domain.Interfaces
{
    public interface ISummaryRepository
    {
         Task<IEnumerable<ResultSummary>> GetReportSummary(DateTime date, int page);
         Task<UserSummary> GetUserSummary(int id, int page);
    }
}