using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class WorkerService : IWorkerService
    {
        protected readonly PortalDbContext _dbContext;
        private readonly ILogger<WorkerService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerService(ILogger<WorkerService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _dbContext = _serviceScopeFactory.CreateScope()
            .ServiceProvider.GetRequiredService<PortalDbContext>();

        }
        public async Task CloseOldSpacesBookings()
        {
            using (var _newContext = new PortalDbContext(_dbContext.Options))
            {
                var spaces = await _newContext.Spaces
                .Where(x => !x.Closed && x.Date < DateTime.Today).ToListAsync();
                if (spaces.Count > 0)
                {
                    foreach (var space in spaces)
                    {
                        var bookings = await _newContext.Bookings.Where(x => x.SpaceId == space.Id && x.Status != BookingStatus.CLOSED.ToString()).ToListAsync();
                        foreach (var booking in bookings)
                        {
                            booking.Status = BookingStatus.CLOSED.ToString();
                        }
                        _logger.LogInformation("Closed all past booking in space {@space}", space.Id);
                        space.Closed = true;
                        _logger.LogInformation("Closed Space {@space}", space.Id);
                        await _newContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}