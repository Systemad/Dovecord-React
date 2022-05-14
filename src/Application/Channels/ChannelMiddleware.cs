using Domain;
using Orleans;
using Orleans.Streams;

namespace Application.Channels;

[ImplicitStreamSubscription(Constants.ChannelNamespace)]
public class ChannelMiddleware : SubscriberGrain
{

    public ChannelMiddleware() : base(Constants.InMemoryStream, Constants.ChannelNamespace)
    {
    }

    public override async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        if (evt is not IEvent obj) return true;
        var wsMsg = new WebSocketMessage(evt.GetType().Name, evt);
        await StreamProvider.GetStream<object>(obj.InvokerUserId, Constants.SignalRNameSpace)
            .OnNextAsync(wsMsg);
        return true;
    }
}