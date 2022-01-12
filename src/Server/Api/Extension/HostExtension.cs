using System;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Extension
{
    public static class HostExtension
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<PortalDbContext>();
            var logger = services.GetRequiredService<ILogger<PortalDbContext>>();
            
            try
            {
                new PortalDbContextSeeder(context).Seed().Wait();
            }
            catch (Exception ex){
                logger.LogError($"An error occurred running migration - {ex.Message}");
            }
            return host;
            }
        }
    }