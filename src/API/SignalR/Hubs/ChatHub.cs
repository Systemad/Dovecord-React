using Microsoft.AspNetCore.SignalR;

namespace Dovecord.SignalR.Hubs;

public class ChatHub : IChatHub
{
    private readonly IHubContext<BaseHub> _hub;

    public ChatHub(IHubContext<BaseHub> hub)
    {
        _hub = hub;
    }

    public Task SendMessageTosServer(Guid serverId, string type, object message) 
        => _hub.Clients.Group(serverId.ToString()).SendAsync(type, message);
    
}