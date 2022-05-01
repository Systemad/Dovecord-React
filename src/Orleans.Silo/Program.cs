// See https://aka.ms/new-console-template for more information

using System.Net;
using Domain;
using Domain.Servers;
using Microsoft.AspNetCore.Builder;
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
    .ConfigureApplicationParts(parts => 
        parts.AddApplicationPart(typeof(ServerGrain).Assembly)
            .AddApplicationPart(typeof(IServerGrain).Assembly)
            .AddApplicationPart(typeof(ISubscriberGrain).Assembly))
    .AddMemoryGrainStorage("PubSubStore")
    .AddSimpleMessageStreamProvider(Constants.InMemoryStream)
    .UseDashboard()
    .ConfigureLogging(
        log => log
            .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
            .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning))
    .Build();

builder.Services.AddSingleton(silo);

var app = builder.Build();

await silo.StartAsync();

await app.RunAsync();
