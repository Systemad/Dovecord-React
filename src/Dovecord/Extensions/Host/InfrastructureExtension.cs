using Dovecord.Databases;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Extensions.Host;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            services.AddDbContext<DoveDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseInMemoryDatabase($"DovecordTesting");
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