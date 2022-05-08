using Application.Channels.Features;
using Application.Servers.Features;
using Domain;
using Domain.Channels;
using Domain.Servers;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Application.Channels;


[ImplicitStreamSubscription(Constants.ChannelNamespace)]
public class ChannelSubscriber : Grain, ISubscriberGrain
{
    private StreamSubscriptionHandle<object>? _sub;
    private IStreamProvider? StreamProvider;
    private readonly IMediator _mediator;

    public ChannelSubscriber(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override async Task OnActivateAsync()
    {
        Console.WriteLine("ChannelSubcsriber activated");
        StreamProvider = GetStreamProvider(Constants.InMemoryStream);

        _sub = await StreamProvider
            .GetStream<object>(this.GetPrimaryKey(), Constants.ChannelNamespace)
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
            case ChannelCreatedEvent obj:
                return await Handle(obj);
            default:
                return false;
        }
    }

    private async Task<bool> Handle(ChannelCreatedEvent evt)
    {
        // Send the server object to persistence store
        var command = new AddServerChannel.AddServerChannelCommand(evt.Channel);
        var commandResponse = await _mediator.Send(command);
        if (commandResponse.Type == 0)
        {
            var serverGrain = GrainFactory.GetGrain<IServerGrain>(commandResponse.ServerId!.Value);
            serverGrain.AddChannelAsync(new AddChannelCommand(commandResponse));
        }
        // Setup SignalR Hub and subscribe to ServerEvents, and if it detects ChannelAddedEvent
        // send DTO to clients
        // send it to next event
        return true;
    }
}