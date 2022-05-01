using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain.Servers;

public interface IServerGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateServerCommand createServerCommand);
    Task JoinServer(Guid ServerId);
}

public class ServerGrain : JournaledGrain<ServerState>, IServerGrain
{
    private IAsyncStream<object> _stream = null!;

    public override Task OnActivateAsync()
    {
        //var streamProvider = GetStreamProvider(Constants.InMemoryStream);
        //_stream = streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace);
        var streamProvider = GetStreamProvider(Constants.InMemoryStream);
        _stream = streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace);
        Console.WriteLine("Grain activated");
        return base.OnActivateAsync();
        //return Task.CompletedTask;
    }

    public async Task CreateAsync(CreateServerCommand createServerCommand)
    {
        Console.WriteLine("CreateAsyncCalled");
        var serverExist = State.Created;
        if (serverExist)
            return; // Send error event
        
        var serverCreatedEvent = new ServerCreatedEvent(createServerCommand.ServerId, createServerCommand.Name, createServerCommand.InvokerUserId);
        RaiseEvent(serverCreatedEvent);
        Console.WriteLine("ServerCreatedEvent Raised");
        await _stream.OnNextAsync(serverCreatedEvent);
    }

    public Task JoinServer(Guid ServerId)
    {
        State.Channels.Add(ServerId);
        return Task.CompletedTask;
    }
}