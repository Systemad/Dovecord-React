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
                Type = OpenApiSecuritySchemeType.OAuth2,
                Name = "Authorization",
                //In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}.",
                Flow = OpenApiOAuth2Flow.Implicit,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = "https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1/oauth2/v2.0/authorize",
                        TokenUrl = "https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1/oauth2/v2.0/token",
                        Scopes = new Dictionary<string, string>
                        {
                            { "https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access", "Access the api as the signed-in user" },
                        }
                    }
                }
            });
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });
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
            endpoints.MapHub<ChatHub>("/chathub");
            endpoints.MapHealthChecks("/api/health");
            endpoints.MapControllers();
        });
        
        app.UseOpenApi();
        app.UseSwaggerUi3(settings =>
        {
            settings.OAuth2Client = new OAuth2ClientSettings
            {
             ClientId = "5a1b9d68-aa2e-4fb7-b91b-7ea8c2cb0ace",
             AppName = "swagger-ui-client"
            };
        });
    }
}