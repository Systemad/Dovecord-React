using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain.Servers;

public interface IServerGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateServerCommand createServerCommand);
}

public class ServerGrain : JournaledGrain<ServerState>, IServerGrain
{
    private IAsyncStream<object> _stream = null!;
    
    public override Task OnActivateAsync()
    {
        var streamProvider = GetStreamProvider(Constants.ServerNamespace);
        _stream = streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace);

        return base.OnActivateAsync();
        //return Task.CompletedTask;
    }

    public async Task CreateAsync(CreateServerCommand createServerCommand)
    {
        var serverExist = State.Created;
        if (serverExist)
            return; // Send error event
        
        var serverCreatedEvent = new ServerCreatedEvent(createServerCommand.ServerId, createServerCommand.Name, createServerCommand.InvokerUserId);
        RaiseEvent(serverCreatedEvent);
        await _stream.OnNextAsync(serverCreatedEvent);
    }
}