using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Extensions.Host;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {

        if (isProduction)
        {
            services.AddDbContext<DoveDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseNpgsql(configuration.GetConnectionString("postgres"),
                    b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
                options.UseSnakeCaseNamingConvention(); 
            });
        }
        else
        {
            services.AddDbContext<DoveDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseInMemoryDatabase($"DovecordTesting");
            });
            
        }
        return services;
    }
}