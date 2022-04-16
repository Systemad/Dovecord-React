using System.Security.Claims;
using Dovecord.Domain.Servers.Features;
using Dovecord.Domain.Users.Features;
using Dovecord.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using Serilog;

namespace Dovecord.SignalR.Hubs;

[Authorize]
[RequiredScope("API.Access")]
public class BaseHub : Hub<IBaseHub>
{
    private static readonly ConnectionMap.ConnectionMapping<Guid> _connections = new();
    
    private readonly IMediator _mediator;
    private readonly IEventQueue _eventQueue;
    public BaseHub(IMediator mediator, IEventQueue eventQueue)
    {
        _mediator = mediator;
        _eventQueue = eventQueue;
    }

    private string? Username => Context.User?.Identity?.Name; //?? "Unknown";
    private Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        var userId = UserId;
        var username = Username;
        Log.Information("SignalR: connected - ConnectionId {ConnectionId}", Context.ConnectionId);
        if (!(userId == Guid.Empty) && !string.IsNullOrEmpty(username))
        {   
            Log.Information("SignalR: starting session");
            IRequest type = new StartUserSessionHandler.StartUserSessionCommand(userId);
            await _eventQueue.Queue(type);
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId.ToString());
            await SubscribeServers();
            _connections.Add(userId, Context.ConnectionId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        var userId = UserId;
        Log.Information("SignalR: disconnected - ConnectionId {ConnectionId}", Context.ConnectionId);
        if (!(UserId == Guid.Empty) && !string.IsNullOrEmpty(Username))
        {
            Log.Information("SignalR: ending session");
            IRequest type = new StopUserSessionHandler.StopUserSessionCommand(userId);
            await _eventQueue.Queue(type);
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId.ToString());
            await UnSubscribeServers();
            _connections.Remove(userId, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(ex);
    }

    /*
    public async Task DeleteMessageById(string messageId)
    {
        await Clients.All.DeleteMessageReceived(messageId);
    }
    */
    //public async Task UserTyping(bool isTyping)
    //    => await Clients.Others.UserTyping(new ActorAction(Username, isTyping));
    
    // INVOKE THESE FROM CLIENT
    public async Task JoinServer(Guid serverId)
    {
        Log.Information("{} joined channel {}", Username, serverId);
        await Groups.AddToGroupAsync(Context.ConnectionId, serverId.ToString());
        var joinedServer = await _mediator.Send(new GetServerById.GetServerByIdGetQuery(serverId));
        await Clients.Group(UserId.ToString()).ServerAction(joinedServer);
    }
    
    public async Task LeaveServer(Guid serverId)
    {
        Log.Information("{} left channel {}", Username, serverId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId.ToString());
    }

    private async Task SubscribeServers()
    {
        var query = new GetServersOfUser.GetServersOfUserQuery();
        var result = await _mediator.Send(query);
        
        foreach (var server in result)
        {
            Console.WriteLine("adding to server");
            await Groups.AddToGroupAsync(Context.ConnectionId, server.Id.ToString());
            // TODO: Send to all server with userDto
            // await Groups.AddToGroupAsync(Context.ConnectionId, server.Id.ToString());
            // await HubHelpers.SendUserActivity(serverId, _hubContext, user);
        }
    }
    
    private async Task UnSubscribeServers()
    {
        var query = new GetServersOfUser.GetServersOfUserQuery();
        var result = await _mediator.Send(query);
        
        foreach (var server in result)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, server.Id.ToString());
        }
    }
}