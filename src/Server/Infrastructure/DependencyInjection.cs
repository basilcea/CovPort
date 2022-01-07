namespace Infrastructure
{
    public static class DependencyInjection
    {
         public static IServiceCollection AddInfrastructure(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
        
            var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
           
            services.AddDbContext<PortalDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbContext"));
                options.UseLoggerFactory(loggerFactory);
            });
            
            // services.AddScoped<IDatabaseService, DbConnection>();
           
            return services;
        }
    }
}