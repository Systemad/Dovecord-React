using Domain;
using Domain.Servers;
using Orleans;
using Orleans.Hosting;

namespace Dovecord.Extensions.Host;

public class ClusterClientHostedService : IHostedService
{
    public IClusterClient Client { get; }
    
    public ClusterClientHostedService(ILoggerProvider loggerProvider)
    {
        Client = new ClientBuilder()
            .UseLocalhostClustering()
            .AddSimpleMessageStreamProvider(Constants.InMemoryStream)
            .ConfigureApplicationParts(parts => parts
                .AddApplicationPart(typeof(ServerGrain).Assembly)
                .AddApplicationPart(typeof(IServerGrain).Assembly))
            .ConfigureApplicationParts(parts => parts.AddFromDependencyContext().WithReferences())
            .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
            .Build();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Client.Connect();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Client.Close();
        Client.Dispose();
    }
}