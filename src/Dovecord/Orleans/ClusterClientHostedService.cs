using Orleans;
using Orleans.Hosting;

namespace Dovecord.Orleans;

public class ClusterClientHostedService : IHostedService
{
    public IClusterClient Client { get; }
    
    public ClusterClientHostedService(ILoggerProvider loggerProvider)
    {
        Client = new ClientBuilder()
            .UseLocalhostClustering()
            .AddSimpleMessageStreamProvider("Chat")
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