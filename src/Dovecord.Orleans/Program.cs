// See https://aka.ms/new-console-template for more information

using Dovecord.Orleans;
using Dovecord.Orleans.Interfaces.User;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

await Host.CreateDefaultBuilder()
    .UseOrleans(
        builder => builder
            .UseLocalhostClustering()
            .AddMemoryGrainStorage("AccountState")
            .AddSimpleMessageStreamProvider("Chat")
            .ConfigureApplicationParts(parts => parts
                .AddApplicationPart(typeof(UserGrain).Assembly)
                .AddApplicationPart(typeof(IUserGrain).Assembly))
            .UseDashboard())
    .ConfigureLogging(
        builder => builder
            .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
            .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning))
    .RunConsoleAsync();
