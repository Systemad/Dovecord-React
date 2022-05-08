using Domain.Servers;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain.Users;

// TODO: Remove MediatR???

// API -> CreateUserModel
// -> Create Send Indivual Values with Record Command
// -> Send it to Grain, create User and send Event, which has DTO and not individual values
// -> Send it through stream, and in SubscriberStream Grain, send it to medaitr command
public interface IUserGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateUserCommand createUserCommand);
    Task JoinServerAsync(JoinServerCommand joinServerCommand);
    Task<bool> UserExists();
}

public class UserGrain : JournaledGrain<UserState>, IUserGrain
{
    private IAsyncStream<object> _stream = null!;

    public override Task OnActivateAsync()
    {
        var streamProvider = GetStreamProvider(Constants.InMemoryStream);
        _stream = streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.UserNamespace);
        Console.WriteLine("Grain activated");
        return base.OnActivateAsync();
    }

    public async Task CreateAsync(CreateUserCommand createUserCommand)
    {
        Console.WriteLine("CreateAsyncCalled");
        var serverExist = State.Created;
        if (serverExist)
            return; // Send error event
        
        var userCreatedEvent = new UserCreatedEvent(createUserCommand.UserId, createUserCommand.Name);
        RaiseEvent(userCreatedEvent);
        Console.WriteLine("UserCreatedEvent Raised");
        await _stream.OnNextAsync(userCreatedEvent);
    }

    public async Task JoinServerAsync(JoinServerCommand joinServerCommand)
    {
        var serverJoinedEvent = new ServerJoinedEvent(joinServerCommand.ServerId, joinServerCommand.InvokerUserId);
        RaiseEvent(serverJoinedEvent);
        await _stream.OnNextAsync(serverJoinedEvent);
    }

    public Task<bool> UserExists() => Task.FromResult(State.Created);
}