using System.Reflection;
using Dovecord.Application.PipelineBehaviors;
using Dovecord.Domain.Messages.Validators;
using Dovecord.Middleware;
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
        services.AddMvc(options => options.Filters.Add<ErrorHandlerFilterAttribute>())
            .AddFluentValidation(cfg => { cfg.AutomaticValidationEnabled = false; });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        return services;
    }
}