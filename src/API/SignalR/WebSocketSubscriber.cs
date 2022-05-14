using Domain;
using Domain.Channels;
using Domain.Messages;
using Domain.Servers;
using Domain.Servers.Dto;
using Dovecord.Extensions.Services;
using Orleans;
using Orleans.Streams;

namespace Dovecord.SignalR;

[ImplicitStreamSubscription(Constants.SignalRNameSpace)]
public class WebSocketSubscriber : Grain
{
    //private IStreamProvider? StreamProvider = null!;
    private IAsyncStream<WebSocketMessage> _asyncStream = null!;
    private StreamSubscriptionHandle<object>? _sub;
    // Is this even possible?
    private readonly ICurrentUserService _currentUserService;
    private Guid _userId;

    public WebSocketSubscriber(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override async Task OnActivateAsync()
    {
        _userId = _currentUserService.UserId;
        var streamProvider = GetStreamProvider(Constants.SignalRNameSpace);
        _sub = await streamProvider
            .GetStream<object>(_currentUserService.UserId, Constants.SignalRNameSpace)
            .SubscribeAsync(HandleAsync);
        await base.OnActivateAsync();
    }

    private async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        switch (evt)
        {
            case ChannelMessage obj:
                return await Handle(obj);
                
            case Channel obj:
                return await Handle(obj);
            
            default:
                return true;
        }
    }

    private async Task<bool> Handle(ChannelMessage message)
    {
        var actor = GrainFactory.GetGrain<IActorGrain>(_userId);
        await actor.ChannelMessage(message);
        return true;
    }
    
    private async Task<bool> Handle(Channel channel)
    {
        var actor = GrainFactory.GetGrain<IActorGrain>(_userId);
        await actor.ChannelCreated(channel);
        return true;
    }
}