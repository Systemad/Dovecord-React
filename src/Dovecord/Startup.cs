using System.Text.Json;
using Dovecord.Databases;
using Dovecord.Seeders;
using Dovecord.SignalR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Dovecord.Extensions.Application;
using Dovecord.Extensions.Host;
using Dovecord.Extensions.Services;
using Dovecord.SignalR.Hubs;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Dovecord;

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
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
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
        services.AddSignalRApplication();
        services.AddCorsService();
        services.AddInfrastructure(_config, _env);
        services.AddApplication();
        services.AddHealthChecks();
        
        services.AddSpaStaticFiles(configuration => 
            configuration.RootPath = "ClientApp/dist");
        
        services.AddOpenApiDocument(configure =>
        {
            configure.DocumentName = "v1";
            configure.Version = "v1";
            configure.Title = "Dovecord API";
            configure.Description = "Backend API for Dovecord";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_env.IsDevelopment())
        {
            using var context = app.ApplicationServices.GetService<DoveDbContext>();
            context.Database.EnsureCreated();
            ChannelSeeder.SeedSampleChannels(app.ApplicationServices.GetService<DoveDbContext>());
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        if (!env.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }
        app.UseRouting();

        app.UseAuthentication(); 
        app.UseAuthorization();

        app.UseSerilogRequestLogging();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/chathub");
            endpoints.MapHealthChecks("/api/health");
            endpoints.MapControllers();
        });
        
        app.UseOpenApi();
        app.UseSwaggerUi3();
    }
}