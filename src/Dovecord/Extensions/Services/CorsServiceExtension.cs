namespace Dovecord.Extensions.Services;

public static class CorsServiceExtension
{
    public static IServiceCollection AddCorsService(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    // React and Blazor client
                    builder.AllowAnyOrigin()
                        .WithOrigins("https://localhost:44480")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });
        return service;
    }
}