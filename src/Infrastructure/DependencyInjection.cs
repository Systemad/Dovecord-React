using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DoveDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(configuration.GetConnectionString("postgres"),
                b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
            //options.UseSqlite(configuration.GetConnectionString("DatabaseConnection"),
            //    b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
        });
        //services.AddScoped<>()
        return services;
    }
}