using System.Security.Claims;
using Application.Servers.Features;
using Domain;
using Domain.Channels;
using Domain.Messages;
using Domain.Users;
using Dovecord.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using Orleans;
using Orleans.Streams;
using Serilog;

namespace Dovecord.SignalR.Hubs;

[Authorize]
[RequiredScope("API.Access")]
public class BaseHub : Hub
{
    private static readonly ConnectionMap.ConnectionMapping<Guid> Connections = new();
    
    private readonly IClusterClient _client;
    private StreamSubscriptionHandle<object> _sub;
    public BaseHub(IClusterClient client)
    {
        _client = client;
    }

    private string? Username => Context.User?.Identity?.Name; //?? "Unknown";
    private Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        var userId = UserId;
        _sub = await _client.GetStreamProvider(Constants.InMemoryStream)
            .GetStream<object>(userId, Constants.SignalRNameSpace)
            .SubscribeAsync(HandleAsync);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {

        await base.OnDisconnectedAsync(ex);
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
        var actor =  _client.GetGrain<IActorGrain>(UserId);
        await actor.ChannelMessage(message);
        return true;
    }
    
    private async Task<bool> Handle(Channel channel)
    {
        var actor = _client.GetGrain<IActorGrain>(UserId);
        await actor.ChannelCreated(channel);
        return true;
    }
}