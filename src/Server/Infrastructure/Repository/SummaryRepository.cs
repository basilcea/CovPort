using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Aggregates;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class SummaryRepository : ISummaryRepository
    {
        protected readonly PortalDbContext _dbContext;
        private readonly ILogger<ResultSummary> _logger;
        public SummaryRepository(PortalDbContext dbContext, ILogger<ResultSummary> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<IEnumerable<ResultSummary>> GetReportSummary(DateTime Date)
        {
            try
            {
                var spaces = _dbContext.Spaces
                .Where(x => DateTime.Parse(x.Date.ToShortDateString()) == Date)
                .AsEnumerable()
                .GroupBy(x => x.LocationName);

                var results = await _dbContext.Results.ToListAsync();

                List<ResultSummary> resultList = new();

                foreach (var space in spaces)
                {
                    var tests = results.Where(x => (
                        x.TestLocation.ToLower() == space.First().LocationName.ToLower()
                        && DateTime.Parse(x.DateCreated.ToShortDateString()) == Date));

                    var resultSummary = new ResultSummary
                    {
                        LocationName = space.First().LocationName,
                        BookingCapacity = space.First().SpacesCreated,
                        Bookings = _dbContext.Bookings.Where(x => x.SpaceId == space.First().Id && x.Status == BookingStatus.PENDING.ToString()).Count(),
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
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }

        }

        public async Task<UserSummary> GetUserSummary(int id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                var bookings = await _dbContext.Bookings.Where(x => x.UserId == id && x.Status == BookingStatus.PENDING.ToString()).ToListAsync();
                var tests = await _dbContext.Results
                .Where(x => x.UserId == id
                && x.Status == TestStatus.COMPLETED.
                ToString()).ToListAsync();
                var result = new UserSummary
                {
                    User = user,
                    Bookings = bookings,
                    Tests = tests
                };
                _logger.LogInformation("Received User Summary: {@response}", result);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
    }
}