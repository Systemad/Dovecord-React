using System.Security.Claims;
using Dovecord.Domain.Entities;
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
    public BaseHub(IStatusService statusService)
    {
        _statusService = statusService;
    }

    private string Username => Context?.User?.Identity?.Name; //?? "Unknown";
    private Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        /*
        if (!(UserId == Guid.Empty) && !string.IsNullOrEmpty(Username))
        {
            await _statusService.OnStartSession(UserId, Username);
            //await Clients.All.UpdateData();
        }
        */

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        /*
        if (!(UserId == Guid.Empty) && !string.IsNullOrEmpty(Username))
        {
            await _statusService.OnStopSession(UserId);
            //await Clients.All.UpdateData();
        }
        */
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
}