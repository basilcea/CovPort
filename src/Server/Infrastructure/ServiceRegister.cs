using Domain.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Infrastructure.Mappings;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<ReaderToClassProfile>());
            services.AddScoped<IEntityRepository<User>, UserRepository>();
            services.AddScoped<IEntityRepository<Result>, ResultRepository>();
            services.AddScoped<IEntityRepository<Booking>, BookingRepository>();
            services.AddScoped<IEntityRepository<Space>, SpaceRepository>();
            services.AddSingleton<IWorkerService, WorkerService>();
            services.AddScoped<ISummaryRepository, SummaryRepository>();
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
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


