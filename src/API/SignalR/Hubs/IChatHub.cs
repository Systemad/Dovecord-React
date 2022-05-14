using Orleans;

namespace Dovecord.SignalR.Hubs;

public interface IChatHub : IGrainObserver
{
    Task SendMessageTosServer(Guid serverId, string type, object message);
}