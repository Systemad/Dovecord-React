using Application.Channels.Features;
using Application.Messages.Features;
using Domain;
using Domain.Channels;
using Domain.Servers;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Application.Channels;

[ImplicitStreamSubscription(Constants.ChannelNamespace)]
public class ChannelSubscriber : SubscriberGrain
{
    private readonly IMediator _mediator;

    public ChannelSubscriber(IMediator mediator) : base(Constants.InMemoryStream, Constants.ChannelNamespace)
    {
        _mediator = mediator;
    }
    
    public override async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        switch (evt)
        {
            case ChannelCreatedEvent obj:
                return await Handle(obj);
            case MessageAddedEvent obj:
                return await Handle(obj);
            default:
                return false;
        }
    }

    private async Task<bool> Handle(ChannelCreatedEvent evt)
    {
        var command = new AddServerChannel.AddServerChannelCommand(evt.Channel);
        await _mediator.Send(command);
        if (evt.Channel.Type == 0)
        {
            var serverGrain = GrainFactory.GetGrain<IServerGrain>(evt.Channel.ServerId!.Value);
            serverGrain.AddChannelAsync(new AddChannelCommand(evt.Channel, evt.Channel.ServerId.Value, evt.InvokerUserId));
        }
        return true;
    }
    
    private async Task<bool> Handle(MessageAddedEvent evt)
    {
        var command = new AddMessage.AddMessageCommandM(evt.Message);
        await _mediator.Send(command);
        return true;
    }
}