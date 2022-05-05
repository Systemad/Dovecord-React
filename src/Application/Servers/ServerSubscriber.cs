﻿using Application.Servers.Features;
using Domain;
using Domain.Servers;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Application.Servers;


[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class ServerSubscriber : Grain, ISubscriberGrain
{
    private StreamSubscriptionHandle<object>? _sub;
    private IStreamProvider? StreamProvider;
    private readonly IMediator _mediator;

    public ServerSubscriber(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override async Task OnActivateAsync()
    {
        Console.WriteLine("ServerSubscribe activated");
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
            case ServerCreatedEvent obj:
                return await Handle(obj);
            case ChannelAddedEvent obj:
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
        // send it to next event
        return true;
    }
    
    private async Task<bool> Handle(ChannelAddedEvent evt)
    {
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
}