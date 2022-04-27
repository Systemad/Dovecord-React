using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool development)
    {

        if (development)
        {
            services.AddDbContext<DoveDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseInMemoryDatabase($"DovecordTesting");
                options.EnableSensitiveDataLogging();
            });
        }
        else
        {
            services.AddDbContext<DoveDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseNpgsql(configuration.GetConnectionString("postgres"),
                    b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
                options.UseSnakeCaseNamingConvention(); 
            });
        }
        return services;
    }
}