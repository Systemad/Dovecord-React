using Domain;
using Orleans;

namespace Dovecord.SignalR;

public interface IPushNotifierGrain : IGrainWithIntegerKey
{
    Task SendMessage(Guid serverId, string type, object message);
}