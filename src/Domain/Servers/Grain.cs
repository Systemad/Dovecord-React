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

public class ServerGrain : EventSourceGrain<ServerState>, IServerGrain
{
    public ServerGrain() : base(Constants.InMemoryStream, Constants.ServerNamespace) { }
    public async Task CreateAsync(CreateServerCommand createServerCommand)
    {
        Console.WriteLine("CreateAsyncCalled");
        var newServer = new Server
        {
            Id = createServerCommand.ServerId,
            Name = createServerCommand.Name,
            IconUrl = null,
            OwnerUserId = createServerCommand.InvokerUserId,
        };
        var serverCreatedEvent = new ServerCreatedEvent(newServer);
        
        var serverExist = State.Created;
        if (serverExist)
            await PublishErrorAsync(serverCreatedEvent);
        
        await PublishEventAsync(serverCreatedEvent);
    }

    public async Task AddChannelAsync(AddChannelCommand addChannelCommand)
    {
        var channelGrain = GrainFactory.GetGrain<IChannelGrain>(addChannelCommand.Channel.Id);
        var exist = await channelGrain.ChannelExist();
        var channelAddedEvent = new ChannelAddedEvent(addChannelCommand.Channel);
        if (exist && !State.Created)
                await PublishErrorAsync(channelAddedEvent);
        await PublishEventAsync(channelAddedEvent);
    }

    public async Task AddUserAsync(AddUserCommand addUserCommand)
    {
        var userAddedEvent = new UserAddedEvent(addUserCommand.ServerId, addUserCommand.UserId);
        var userGrain = GrainFactory.GetGrain<IUserGrain>(addUserCommand.UserId);
        var exist = await userGrain.UserExists();
        var alreadyAdded = State.Users.Contains(addUserCommand.UserId);
        if (!exist || alreadyAdded)
            await PublishErrorAsync(userAddedEvent);

        await PublishEventAsync(userAddedEvent);
    }

    public Task<bool> ServerExist() => Task.FromResult(State.Created);

}