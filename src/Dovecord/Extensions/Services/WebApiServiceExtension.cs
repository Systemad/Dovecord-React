using System.Reflection;
using Dovecord.Application.PipelineBehaviors;
using Dovecord.Domain.Messages.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace Dovecord.Extensions.Services;

public static class WebApiServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddMediatR(typeof(Startup));
        services.AddMvc().AddFluentValidation();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        return services;
    }
}