using System.Reflection;
using Application.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
    
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        bool development)
    {
        services.AddDbContext<DoveDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(configuration.GetConnectionString("postgres"),
                    b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
        });
    }
}