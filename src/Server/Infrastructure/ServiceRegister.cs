using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.Aggregates;
using Domain.Entities;
using Application.DTO;
using System.Linq;
using System;

namespace Infrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IEntityRepository<User>, UserRepository>();
            services.AddScoped<IEntityRepository<Result>, ResultRepository>();
            services.AddScoped<IEntityRepository<Booking>, BookingRepository>();
            services.AddScoped<IEntityRepository<Space>, SpaceRepository>();
            services.AddScoped<ISummaryRepository, ReportRepository>();
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IDbService, DbConnection>();
            var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            services.AddDbContext<PortalDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
                options.UseLoggerFactory(loggerFactory);
            });
 
            
            
            return services;
        }

    }
}


