using Orleans;

namespace Dovecord.Domain.Servers;

[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class ServerSubscriber : Grain, IGrainWithGuidKey
{
    
}