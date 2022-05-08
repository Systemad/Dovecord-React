using Application.Servers.Features;
using Application.Users.Features;
using Domain;
using Domain.Servers;
using Domain.Users;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Application.Users;


[ImplicitStreamSubscription(Constants.UserNamespace)]
public class UserSubscriber : Grain, ISubscriberGrain
{
    private StreamSubscriptionHandle<object>? _sub;
    private IStreamProvider? StreamProvider;
    private readonly IMediator _mediator;

    public UserSubscriber(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override async Task OnActivateAsync()
    {
        Console.WriteLine("UserSubscriber activated");
        StreamProvider = GetStreamProvider(Constants.InMemoryStream);

        _sub = await StreamProvider
            .GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace)
            .SubscribeAsync(HandleAsync);
        await base.OnActivateAsync();
    }
    
    public override async Task OnDeactivateAsync()
    {
        await _sub!.UnsubscribeAsync();
        await base.OnDeactivateAsync();
    }

    private async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        switch (evt)
        {
            case ServerJoinedEvent obj:
                return await Handle(obj);
            case UserCreatedEvent obj:
                return await Handle(obj);
            default:
                return false;
        }
    }

    private async Task<bool> Handle(ServerJoinedEvent evt)
    {
        var command = new AddUserToServer.AddUserToServerCommand(evt.ServerId, evt.ServerId);
        await _mediator.Send(command);
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
    private async Task<bool> Handle(UserCreatedEvent evt)
    {
        var command = new AddUser.AddUserCommand(evt.Id, evt.Name);
        await _mediator.Send(command);
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
}