using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerService _worker;

        public Worker(ILogger<Worker> logger, IWorkerService worker)
        {
            _logger = logger;
            _worker = worker;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                var interval = TimeSpan.FromHours(24);
                var nextRunTime = DateTime.Today.AddDays(1);
                var currentTime = DateTime.Now;
                var firstInterval = nextRunTime.Subtract(currentTime);

                Action action = () =>
                {
                    var t1 = Task.Delay(firstInterval);
                    t1.Wait();
                    TimerAction(null);
                    _logger.LogInformation("Worker Closing Old Spaces and Bookings: {time}", DateTimeOffset.Now);
                    var timer = new Timer(TimerAction, null, TimeSpan.Zero, interval);
                };
                Task.Run(action); 
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
        private void TimerAction(object state)
        {
            _worker.CloseOldSpacesBookings();
        }


    }
}