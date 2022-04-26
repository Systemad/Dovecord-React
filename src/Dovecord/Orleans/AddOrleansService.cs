using Orleans;

namespace Dovecord.Orleans;

public static class AddOrleansService
{
    public static void AddOrleans(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ClusterClientHostedService>();
        serviceCollection.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
        serviceCollection.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);
        serviceCollection.AddSingleton<IGrainFactory>(_ => _.GetService<ClusterClientHostedService>().Client);
    }
}

