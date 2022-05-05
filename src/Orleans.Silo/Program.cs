using System.Net;
using System.Reflection;
using Application.Database;
using Application.Servers;
using Domain;
using Domain.Servers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

var silo = new SiloHostBuilder()
    .UseLocalhostClustering()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "dovecord-service";
    })
    .Configure<EndpointOptions>(options =>
    {
        options.AdvertisedIPAddress = IPAddress.Loopback;
    })
    .AddMemoryGrainStorageAsDefault()
    .ConfigureApplicationParts(parts => parts.AddFromDependencyContext().WithReferences())
    .AddMemoryGrainStorage("PubSubStore")
    .AddSimpleMessageStreamProvider(Constants.InMemoryStream)
    .UseDashboard()
    .ConfigureLogging(
        log => log
            .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
            .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning))
    .ConfigureServices(services =>
    {
        /*
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        */
        services.AddValidatorsFromAssemblyContaining(typeof(ServerGrain));
        services.AddMediatR(typeof(ServerGrain).Assembly);
        
        services.AddValidatorsFromAssemblyContaining(typeof(ServerSubscriber));
        services.AddMediatR(typeof(ServerSubscriber).Assembly);
        
        services.AddDbContext<DoveDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"),
                b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
            //options.UseSnakeCaseNamingConvention(); 
        });
    })
    .Build();

builder.Services.AddSingleton(silo);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

//var dbcontext = app.Services.GetRequiredService<DoveDbContext>();
//dbcontext.Database.EnsureDeleted();
//dbcontext.Database.EnsureCreated();
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DoveDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
}
*/
await silo.StartAsync();
await app.RunAsync();
