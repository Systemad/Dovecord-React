using Domain;
using Dovecord.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Orleans;
using Orleans.Concurrency;

namespace Dovecord.SignalR;

[Reentrant]
[StatelessWorker]
public class PushNotifierGrain: Grain, IPushNotifierGrain
{
    private readonly ChatHub _chatHub;

    public PushNotifierGrain(IHubContext<BaseHub> hubContext)
    {
        _chatHub = new ChatHub(hubContext);
    }
    
    public override async Task OnActivateAsync()
    {
        await base.OnActivateAsync();
    }

    public async Task SendMessage(Guid serverId, string type, object message)
    {
        if (message is null)
            return;
        
        await _chatHub.SendMessageTosServer(serverId, type, message);
    }
}