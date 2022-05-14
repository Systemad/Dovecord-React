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
public class UserSubscriber : SubscriberGrain
{
    private readonly IMediator _mediator;

    public UserSubscriber(IMediator mediator) : base(Constants.InMemoryStream, Constants.UserNamespace)
    {
        _mediator = mediator;
    }
    
    public override async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
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
        return true;
    }
    private async Task<bool> Handle(UserCreatedEvent evt)
    {
        var command = new AddUser.AddUserCommand(evt.Id, evt.Name);
        await _mediator.Send(command);
        return true;
    }
}