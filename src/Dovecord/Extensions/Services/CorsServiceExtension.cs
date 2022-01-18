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
                    builder.AllowAnyOrigin()
                        .WithOrigins("https://localhost:44418")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    //builder.AllowAnyHeader();
                    //builder.AllowAnyMethod();
                    //builder.DisallowCredentials();
                });
        });
        return service;
    }
}