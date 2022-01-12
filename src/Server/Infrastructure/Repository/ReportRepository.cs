using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ReportRepository : ISummaryRepository
    {
        protected readonly PortalDbContext DbContext;
        public ReportRepository(PortalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<IEnumerable<ResultSummary>> GetReportSummary(DateTime Date)
        {
            var spaces = DbContext.Spaces .AsEnumerable()
            .Where(x => DateTime.Parse(x.Date.ToShortDateString()) == Date)
            .GroupBy(x => x.LocationName);

            List<ResultSummary> resultList = new();

            foreach (var space in spaces)
            {
                var tests = await DbContext.Results.AsQueryable().Where(x => (
                    x.TestLocation.ToLower() == space.First().LocationName.ToLower()
                    && DateTime.Parse(x.DateCreated.ToShortDateString()) == Date))
                    .ToListAsync();

                var resultSummary = new ResultSummary
                {
                    LocationName = space.First().LocationName,
                    BookingCapacity = space.First().SpacesCreated,
                    Bookings = DbContext.Bookings.Where(x => x.SpaceId == space.First().Id).Count(),
                    Tests = tests.Count(),
                    PositiveCases = tests
                    .Where(x => x.Status == TestStatus.COMPLETED.ToString() && x.Positive == true)
                    .Count(),
                    NegativeCases = tests
                    .Where(x => x.Status == TestStatus.COMPLETED.ToString() && x.Positive == false)
                    .Count(),
                    AwaitingResult = tests
                    .Where(x => x.Status == TestStatus.PENDING.ToString() && x.Positive == null)
                    .Count()
                };

                resultList.Add(resultSummary);
            };
            return resultList;

        }

    }
}