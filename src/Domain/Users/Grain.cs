using Orleans;

namespace Domain.Users;

public interface IUserGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateUserCommand createUserCommand);
    Task JoinServerAsync(JoinServerCommand joinServerCommand);
    Task<bool> UserExists();
}

public class UserGrain : EventSourceGrain<UserState>, IUserGrain
{
    public UserGrain() : base(Constants.InMemoryStream, Constants.UserNamespace){}
    public async Task CreateAsync(CreateUserCommand createUserCommand)
    {
        var userCreatedEvent = new UserCreatedEvent(createUserCommand.UserId, createUserCommand.Name);
        var task = State.Created ? PublishEventAsync(userCreatedEvent) : PublishErrorAsync(userCreatedEvent);
        await task;
    }

    public async Task JoinServerAsync(JoinServerCommand joinServerCommand)
    {
        var serverJoinedEvent = new ServerJoinedEvent(joinServerCommand.ServerId, joinServerCommand.InvokerUserId);
        await PublishEventAsync(serverJoinedEvent);
    }

    public Task<bool> UserExists() => Task.FromResult(State.Created);
}