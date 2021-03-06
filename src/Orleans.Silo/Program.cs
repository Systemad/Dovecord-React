using System.Net;
using System.Reflection;
using Application.Channels;
using Application.Database;
using Application.Servers;
using Application.Users;
using Domain;
using Domain.Channels;
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
    .ConfigureApplicationParts(parts => parts.AddFromDependencyContext().WithReferences())
    .ConfigureServices(services =>
    {
        // -- Server
        services.AddValidatorsFromAssemblyContaining(typeof(ServerGrain));
        services.AddMediatR(typeof(ServerGrain).Assembly);
        
        services.AddValidatorsFromAssemblyContaining(typeof(ServerSubscriber));
        services.AddMediatR(typeof(ServerSubscriber).Assembly);
        
        // --- Channel
        services.AddValidatorsFromAssemblyContaining(typeof(ChannelGrain));
        services.AddMediatR(typeof(ChannelSubscriber).Assembly);
        
        // -- User
        services.AddValidatorsFromAssemblyContaining(typeof(UserSubscriber));
        services.AddMediatR(typeof(UserSubscriber).Assembly);
        
        services.AddValidatorsFromAssemblyContaining(typeof(UserManageSaga));
        services.AddMediatR(typeof(UserManageSaga).Assembly);
        
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

await silo.StartAsync();
await app.RunAsync();
