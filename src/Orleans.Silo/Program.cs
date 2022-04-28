// See https://aka.ms/new-console-template for more information

using Domain;
using Domain.Servers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

await Host.CreateDefaultBuilder()
    .UseOrleans(
        builder => builder
            .UseLocalhostClustering()
            .AddMemoryGrainStorage("PubSubStorage")
            .AddSimpleMessageStreamProvider(Constants.InMemoryStream)
            .ConfigureApplicationParts(parts => parts
                .AddApplicationPart(typeof(ServerGrain).Assembly)
                .AddApplicationPart(typeof(IServerGrain).Assembly))
            .UseDashboard())
    .ConfigureLogging(
        builder => builder
            .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
            .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning))
    .RunConsoleAsync();
