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
using NSwag.AspNetCore;
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
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //options.JsonSerializerOptions.ReferenceHandler = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            //options.SerializerSettings. PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // use default version when version is not specified
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        services.AddAppAuthentication(_config);
        services.AddSignalRApplication();
        services.AddCorsService();
        services.AddApplication();
        services.AddInfrastructure(_config, _env);
        services.AddHealthChecks();
        
        services.AddSpaStaticFiles(configuration => 
            configuration.RootPath = "dovecord-react/dist");

        services.AddOpenApiServiceOath();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_env.IsDevelopment())
        {
            using var context = app.ApplicationServices.GetService<DoveDbContext>();
            context.Database.EnsureCreated();
            //ChannelSeeder.SeedSampleChannels(app.ApplicationServices.GetService<DoveDbContext>());
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
            endpoints.MapHub<BaseHub>("/chathub");
            endpoints.MapHealthChecks("/api/health");
            endpoints.MapControllers();
        });
        
        app.UseOpenApi();
        app.UseSwaggerUi3(settings =>
        {
            settings.OAuth2Client = new OAuth2ClientSettings
            {
             ClientId = _config["Swagger:ClientId"],
             AppName = "swagger-ui-client"
            };
        });
    }
}