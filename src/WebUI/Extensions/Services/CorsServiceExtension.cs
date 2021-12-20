namespace WebUI.Extensions.Services;

public static class CorsServiceExtension
{
    public static IServiceCollection AddCorsService(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
        });
        return service;
    }
}