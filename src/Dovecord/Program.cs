using Autofac.Extensions.DependencyInjection;
using Serilog;
using Dovecord.Extensions.Host;
using Dovecord.Orleans.User;
using Orleans;
using Orleans.Hosting;

namespace Dovecord;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.AddLoggingConfiguration();
        try
        {
            Log.Information("Starting application");
            await host.RunAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "The application failed to start correctly");
            throw;
        }
        finally
        {
            Log.Information("Shutting down application");
            Log.CloseAndFlush();
        }
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseOrleans(builder =>
            {
                builder.UseLocalhostClustering()
                    .ConfigureApplicationParts(parts => parts
                        .AddApplicationPart(typeof(UserGrain).Assembly).WithReferences()
                        .AddApplicationPart(typeof(IUserGrain).Assembly).WithReferences())
                    .AddMemoryGrainStorage("UserState")
                    .ConfigureLogging(
                        log => log
                            .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
                            .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning))
                    .UseDashboard();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                    //.UseContentRoot(Directory.GetCurrentDirectory());
            });
}