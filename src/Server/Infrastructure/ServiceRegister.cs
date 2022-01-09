using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegister
    {
         public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(FakeEntityRepository<>));
            services.AddScoped<IDbService, FakeDbConnection>();
            return services;
        }
    }
}