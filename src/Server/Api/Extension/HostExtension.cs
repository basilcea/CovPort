using System;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api.Extension
{
    public static class HostExtension
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<PortalDbContext>();
            try
            {
                new PortalDbContextSeeder(context).Seed().Wait();
            }
            catch (Exception ex){
                
            }
            return host;
            }
        }
    }