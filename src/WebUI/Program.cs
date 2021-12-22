using System.Reflection;
using Serilog;
using WebUI.Extensions.Host;

namespace WebUI;

public class Program
{
    public async static Task Main(string[] args)
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
            //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                    //.UseContentRoot(Directory.GetCurrentDirectory());
            });
}