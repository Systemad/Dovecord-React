using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebUI.Extensions.Host;
using WebUI.Extensions.Services;

namespace WebUI;

public class Startup
{
    public IConfiguration _config { get; }
    public IWebHostEnvironment _env { get;  }

    public Startup(IWebHostEnvironment env, IConfiguration config)
    {
        _env = env;
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Log.Logger);
        services.AddControllers();
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // use default version when version is not specified
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });
        
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        services.AddAppAuthentication(_config);
        services.AddCorsService();
        services.AddInfrastructure(_config, _env);
        services.AddApplication();
        
        services.AddHealthChecks();
        services.AddOpenApiDocument(configure =>
        {
            configure.DocumentName = "v1";
            configure.Version = "v1";
            configure.Title = "Dovecord API";
            configure.Description = "Backend API for Dovecord";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_env.IsDevelopment())
        {
            // Add channel seed
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication(); 
        app.UseAuthorization();

        app.UseSerilogRequestLogging();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/api/health");
            endpoints.MapControllers();
        });
        
        app.UseOpenApi();
        app.UseSwaggerUi3();
    }
}