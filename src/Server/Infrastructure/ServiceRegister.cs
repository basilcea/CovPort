using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
            services.AddScoped<IDbService, DbConnection>();
            services.AddDbContext<PortalDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            return services;
        }
    }
}