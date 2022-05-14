using Domain.Channels;
using Domain.Messages;
using Domain.Servers;
using Orleans;
using Orleans.Concurrency;

namespace Dovecord.SignalR;

public interface IActorGrain : IGrainWithGuidKey
{
    Task ChannelCreated(Channel channel);
    Task ChannelDeleted(Guid serverId, Guid channelId);
    
    Task ChannelMessage(ChannelMessage message);
    Task ChannelMessageDeleted(Guid serverId, Guid messageId);
}

[Reentrant]
public class ActorGrain : Grain, IActorGrain
{

    private object _lastMessage = null!;
    
    public ActorGrain()
    {
    }

    public override Task OnActivateAsync()
    {
        return base.OnActivateAsync();
    }
    
    public async Task ChannelCreated(Channel channel)
    {
        if (_lastMessage is not null)
        {
            _lastMessage = channel;
            var notifier = GrainFactory.GetGrain<IPushNotifierGrain>(0);
            await notifier.SendMessage(channel.ServerId.Value, nameof(channel), channel);    
        }
    }

    public async Task ChannelDeleted(Guid serverId, Guid channelId)
    {
        throw new NotImplementedException();
    }

    public async Task ChannelMessage(ChannelMessage message)
    {
        throw new NotImplementedException();
    }

    public async Task ChannelMessageDeleted(Guid serverId, Guid messageId)
    {
        throw new NotImplementedException();
    }
}