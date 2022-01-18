using System.Reflection;
using MediatR;

namespace Dovecord.Extensions.Services;

public static class WebApiServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddMediatR(typeof(Startup));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    } 
}