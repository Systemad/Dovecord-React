using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Extensions.Host;
using WebUI.Extensions.Services;

namespace FunctionalTests;

public class Startup
{
    public IConfiguration _config { get; }
    public IWebHostEnvironment _env { get; }
    
    public WebApplication _WebApplication { get; set; }
    
    public Startup(IConfiguration config, IWebHostEnvironment env)
    {
        _config = config;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
       //CreateBuilder
        
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        services.AddInfrastructure(_config, false);
        services.AddControllers();
        //services.AddApiVersioningExtension();
        services.AddApplication();
        services.AddHealthChecks();
        services.AddSwaggerExtension();
    }
}