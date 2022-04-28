using Dovecord.Extensions.Host;
using Orleans;

namespace Dovecord.Extensions.Services;

public static class AddOrleansService
{
    public static void AddOrleans(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ClusterClientHostedService>();
        serviceCollection.AddSingleton<IHostedService>(_ => _.GetRequiredService <ClusterClientHostedService>());
        serviceCollection.AddSingleton(_ => _.GetRequiredService<ClusterClientHostedService>().Client);
        serviceCollection.AddSingleton<IGrainFactory>(_ => _.GetRequiredService<ClusterClientHostedService>().Client);
    }
}

