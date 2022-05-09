using Application.Servers.Features;
using Application.Users.Features;
using Domain;
using Domain.Servers;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Application.Servers;


[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class ServerSubscriber : SubscriberGrain
{
    private readonly IMediator _mediator;

    public ServerSubscriber(IMediator mediator) : base(Constants.InMemoryStream, Constants.ServerNamespace)
    {
        _mediator = mediator;
    }

    public override async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        switch (evt)
        {
            case ServerCreatedEvent obj:
                return await Handle(obj);
            case ChannelAddedEvent obj:
                return await Handle(obj);
            case UserAddedEvent obj:
                return await Handle(obj);
            default:
                return false;
        }
    }

    private async Task<bool> Handle(ServerCreatedEvent evt)
    {
        // Send the server object to persistence store
        var command = new AddServer.AddServerCommand(evt.Server);
        var commandResponse = await _mediator.Send(command);
        // send it to next event??
        return true;
    }
    
    private async Task<bool> Handle(ChannelAddedEvent evt)
    {
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
    
    private async Task<bool> Handle(UserAddedEvent evt)
    {
        var command = new AddUserToServer.AddUserToServerCommand(evt.ServerId, evt.UserId);
        await _mediator.Send(command);
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
}