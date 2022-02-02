using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Application.Exceptions;
using AutoMapper;
using Domain.Aggregates;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class SummaryRepository : ISummaryRepository
    {
        protected readonly PortalDbContext _dbContext;
        private readonly ILogger<ResultSummary> _logger;
        private readonly IMapper _mapper;
        public SummaryRepository(PortalDbContext dbContext, ILogger<ResultSummary> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public static void AddParameters(DbCommand cmd, string name, object value, DbType dbType)
        {

            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            param.DbType = dbType;
            cmd.Parameters.Add(param);
        }
        public async Task<IEnumerable<ResultSummary>> GetReportSummary(DateTime Date, int page)
        {
            try
            {
                var results = new List<ResultSummary>();
                var query = File.ReadAllText("../Infrastructure/SQL/ReportViewQuery.sql");
                var connection = _dbContext.Database.GetDbConnection();
                var cmd = connection.CreateCommand();
                AddParameters(cmd, "Date", Date, DbType.DateTime);
                AddParameters(cmd, "PageNumber", page, DbType.Int32);
                AddParameters(cmd, "PageSize", 50, DbType.Int32);
                cmd.CommandText = query;
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(_mapper.Map<ResultSummary>(reader));
                }
                reader.Close();
                _logger.LogInformation("Received Report Summary: {@response}", results);
                return await Task.FromResult(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }

        }

        public async Task<UserSummary> GetUserSummary(int id, int page)
        {
            try
            {
                var userWritten = false;
                var bookings = new List<Booking>();
                var testResults = new List<Result>();
                var user = new User();
                var query = File.ReadAllText("../Infrastructure/SQL/UserSummaryViewQuery.sql");
                var connection = _dbContext.Database.GetDbConnection();
                var cmd = connection.CreateCommand();
                AddParameters(cmd, "UserId", id, DbType.Int32);
                AddParameters(cmd, "PageNumber", page, DbType.Int32);
                AddParameters(cmd, "PageSize", 10, DbType.Int32);
                cmd.CommandText = query;
                connection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!userWritten)
                    {
                        user = _mapper.Map<User>(reader);
                        userWritten = true;
                    }
                    if (reader[8].ToString() == "PENDING")
                    {
                        bookings.Add(_mapper.Map<Booking>(reader));
                    }
                    if (reader[13] is not DBNull)
                    {
                        testResults.Add(_mapper.Map<Result>(reader));
                    }

                }

                reader.Close();
                var result = new UserSummary
                {
                    User = user,
                    Bookings = bookings,
                    Tests = testResults
                };

                _logger.LogInformation("Received User Summary: {@response}", result);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
    }
}