using Domain;
using Orleans;
using Orleans.Streams;

namespace Application.Servers;

[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class ServerMiddleware : SubscriberGrain
{

    public ServerMiddleware() : base(Constants.InMemoryStream, Constants.ServerNamespace)
    {
    }
    
    public override async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        if (evt is not IEvent obj) return true;
        await StreamProvider.GetStream<object>(obj.InvokerUserId, Constants.SignalRNameSpace)
            .OnNextAsync(evt);
        return true;
    }
}