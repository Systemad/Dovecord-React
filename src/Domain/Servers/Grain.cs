using Domain.Channels;
using Domain.Users;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain.Servers;

public interface IServerGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateServerCommand createServerCommand);
    Task AddChannelAsync(AddChannelCommand addChannelCommand);
    Task AddUserAsync(AddUserCommand addUserCommand);
    Task<bool> ServerExist();
}

public class ServerGrain : JournaledGrain<ServerState>, IServerGrain
{
    private IAsyncStream<object> _stream = null!;

    public override Task OnActivateAsync()
    {
        var streamProvider = GetStreamProvider(Constants.InMemoryStream);
        _stream = streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace);
        Console.WriteLine("Grain activated");
        return base.OnActivateAsync();
    }

    public async Task CreateAsync(CreateServerCommand createServerCommand)
    {
        Console.WriteLine("CreateAsyncCalled");
        var serverExist = State.Created;
        if (serverExist)
            return; // Send error event
        var newServer = new Server
        {
            Id = createServerCommand.ServerId,
            Name = createServerCommand.Name,
            IconUrl = null,
            OwnerUserId = createServerCommand.InvokerUserId,
        };
        var serverCreatedEvent = new ServerCreatedEvent(newServer);
        RaiseEvent(serverCreatedEvent);
        Console.WriteLine("ServerCreatedEvent Raised");
        await _stream.OnNextAsync(serverCreatedEvent);
    }

    public async Task AddChannelAsync(AddChannelCommand addChannelCommand)
    {
        var channelGrain = GrainFactory.GetGrain<IChannelGrain>(addChannelCommand.Channel.Id);
        var exist = await channelGrain.ChannelExist();
        if (exist && !State.Created)
            return; // error;
        
        var channelAddedEvent = new ChannelAddedEvent(addChannelCommand.Channel);
        RaiseEvent(channelAddedEvent);
        await _stream.OnNextAsync(channelAddedEvent);
    }

    public async Task AddUserAsync(AddUserCommand addUserCommand)
    {
        var userGrain = GrainFactory.GetGrain<IUserGrain>(addUserCommand.UserId);
        var exist = await userGrain.UserExists();
        if (!exist) // publish error
            return;
        
        var alreadyAdded = State.Users.Contains(addUserCommand.UserId);
        if (alreadyAdded)
            return; // Send error event
        var userAddedEvent = new UserAddedEvent(addUserCommand.ServerId, addUserCommand.UserId);
        RaiseEvent(userAddedEvent);
        await _stream.OnNextAsync(userAddedEvent);
    }

    public Task<bool> ServerExist() => Task.FromResult(State.Created);

}