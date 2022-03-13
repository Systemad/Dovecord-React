using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Dovecord.Extensions.Services;

public static class AddOpenApi
{
    public static IServiceCollection AddOpenApiServiceJwt(this IServiceCollection services)
    {
        services.AddOpenApiDocument(configure =>
        {
            configure.DocumentName = "v1";
            configure.Version = "v1";
            configure.Title = "Dovecord API";
            configure.Description = "Backend API for Dovecord";
            configure.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Type = OpenApiSecuritySchemeType.Http,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}.",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });

        return services;
    }
    
    public static IServiceCollection AddOpenApiServiceOath(this IServiceCollection services)
    {
        services.AddOpenApiDocument(configure =>
        {
            configure.DocumentName = "v1";
            configure.Version = "v1";
            configure.Title = "Dovecord API";
            configure.Description = "Backend API for Dovecord";
            configure.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Name = "Authorization",
                //In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}.",
                Scheme = "Bearer",
                Flow = OpenApiOAuth2Flow.Implicit,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = "https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1/oauth2/v2.0/authorize",
                        TokenUrl = "https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1/oauth2/v2.0/token",
                        Scopes = new Dictionary<string, string>
                        {
                            {
                                "https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access",
                                "Access the api as the signed-in user"
                            }
                        }
                    }
                }
            });
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });
        return services;
    }
}