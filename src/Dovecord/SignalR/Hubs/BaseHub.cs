using System.Security.Claims;
using Dovecord.Domain.Entities;
using Dovecord.Domain.Servers.Features;
using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
using Dovecord.Domain.Users.Services;
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
    private readonly IStatusService _statusService;
    private readonly IMediator _mediator;
    public BaseHub(IStatusService statusService, IMediator mediator)
    {
        _statusService = statusService;
        _mediator = mediator;
    }

    private string? Username => Context.User?.Identity?.Name; //?? "Unknown";
    private Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        var userId = UserId;
        var username = Username;
        Log.Information("SignalR: connected - connectionId {}", Context.ConnectionId);
        if (!(userId == Guid.Empty) && !string.IsNullOrEmpty(username))
        {   
            Log.Information("SignalR: starting session");
            await _statusService.OnStartSession(UserId, Username);
            await SubscribeServers();
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        Log.Information("SignalR: disconnected");
        if (!(UserId == Guid.Empty) && !string.IsNullOrEmpty(Username))
        {
            Log.Information("SignalR: ending session");
            await _statusService.OnStopSession(UserId);
            await UnSubscribeServers();
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
    
    public async Task JoinChannel(Guid channelId)
    {
        Log.Information("{} joined channel {}", Username, channelId);
        await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
    }
         
    public async Task LeaveChannel(Guid channelId)
    {
        Log.Information("{} left channel {}", Username, channelId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId.ToString());
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