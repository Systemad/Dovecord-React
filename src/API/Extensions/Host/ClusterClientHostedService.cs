using Domain;
using Domain.Servers;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Dovecord.Extensions.Host;

public class ClusterClientHostedService : IHostedService, IAsyncDisposable, IDisposable
{
    public IClusterClient Client { get; }
    
    public ClusterClientHostedService(ILoggerProvider loggerProvider)
    {
        Client = new ClientBuilder()
            // TODO: Add ADO.NET Clustering to deployment
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "dovecord-service";
            })
            .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
            .AddSimpleMessageStreamProvider(Constants.InMemoryStream)
            .ConfigureApplicationParts(parts => 
                parts.AddApplicationPart(typeof(IServerGrain).Assembly)
                .AddApplicationPart(typeof(ISubscriberGrain).Assembly))
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
    
    public void Dispose() => Client?.Dispose();
    public ValueTask DisposeAsync() => Client?.DisposeAsync() ?? default;
}