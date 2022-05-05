using Application.Database;
using Dovecord.Extensions.Host;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dovecord;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.AddLoggingConfiguration();
        
        
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DoveDbContext>();
                await context.Database.MigrateAsync();
                //context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

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
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                    //.UseContentRoot(Directory.GetCurrentDirectory());
            });
}