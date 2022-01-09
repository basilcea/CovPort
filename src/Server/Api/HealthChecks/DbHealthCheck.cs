using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthChecks
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly IDbService _databaseService;

        public DbHealthCheck(IDbService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var pingResult = await _databaseService.Ping();
            if (pingResult.Success)
            {
                return HealthCheckResult.Healthy();
            }

            return new HealthCheckResult(context.Registration.FailureStatus, pingResult.Message);
        }
    }
}
