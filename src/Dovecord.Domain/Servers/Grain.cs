using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Dovecord.Domain.Servers;

public interface IServerGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateServerCommand createServerCommand);
}

public class ServerGrain : JournaledGrain<ServerState>, IServerGrain
{
    private IStreamProvider StreamProvider;
    
    public override Task OnActivateAsync()
    {
        StreamProvider = GetStreamProvider(Constants.InMemoryStream);
        return Task.CompletedTask;
    }

    public async Task CreateAsync(CreateServerCommand createServerCommand)
    {
        var serverExist = State.Created;
        var serverCreatedEvent = new ServerCreatedEvent(createServerCommand.ServerId, createServerCommand.Name);
        if(serverExist) RaiseEvent(serverCreatedEvent);
        await StreamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace)
            .OnNextAsync(serverCreatedEvent);
    }
}