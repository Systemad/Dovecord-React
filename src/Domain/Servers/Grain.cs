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
        var newServer = new Server
        {
            Id = createServerCommand.ServerId,
            Name = createServerCommand.Name,
            IconUrl = null,
            OwnerUserId = createServerCommand.InvokerUserId,
        };
        var serverCreatedEvent = new ServerCreatedEvent(newServer, createServerCommand.InvokerUserId);
        var task = State.Created ? PublishEventAsync(serverCreatedEvent) : PublishErrorAsync(serverCreatedEvent);
        await task;
    }

    public async Task AddChannelAsync(AddChannelCommand addChannelCommand)
    {
        var channelGrain = GrainFactory.GetGrain<IChannelGrain>(addChannelCommand.Channel.Id);
        var exist = await channelGrain.ChannelExist();
        var channelAddedEvent = new ChannelAddedEvent(addChannelCommand.Channel, addChannelCommand.InvokerUserId);
        var valid = exist && !State.Created;
        
        var task = valid ? PublishEventAsync(channelAddedEvent) : PublishErrorAsync(channelAddedEvent);
        await task;
    }

    public async Task AddUserAsync(AddUserCommand addUserCommand)
    {
        var userAddedEvent = new UserAddedEvent(addUserCommand.ServerId, addUserCommand.InvokerUserId);
        var userGrain = GrainFactory.GetGrain<IUserGrain>(addUserCommand.InvokerUserId);
        var exist = await userGrain.UserExists();
        var valid = !exist || State.Users.Contains(addUserCommand.InvokerUserId);;

        var task = valid ? PublishEventAsync(userAddedEvent) : PublishErrorAsync(userAddedEvent);
        await task;
    }

    public Task<bool> ServerExist() => Task.FromResult(State.Created);

}