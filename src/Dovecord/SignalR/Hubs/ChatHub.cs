using System.Security.Claims;
using Dovecord.Domain.Entities;
using Dovecord.Domain.Users.Features;
using Dovecord.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using Serilog;

namespace Dovecord.SignalR.Hubs;

[Authorize]
[RequiredScope("API.Access")]
public class ChatHub : Hub<IChatClient>
{
    private readonly IMediator _mediator;
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string Username => Context?.User?.Identity?.Name ?? "Unknown";
    private Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        var updateUser = new UpdateUser.UpdateUserCommand(UserId, new UserManipulationDto { IsOnline = true });
        var userExist = await _mediator.Send(updateUser);
        if (!userExist)
        {
            var addUser = new AddUser.AddUserCommand(new UserCreationDto
            {
                Name = Username,
                IsOnline = true
            });
            await _mediator.Send(addUser);
        }
        await Clients.All.UpdateData();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        var updateUser = new UpdateUser.UpdateUserCommand(UserId, new UserManipulationDto { IsOnline = false });
        await _mediator.Send(updateUser);
        await Clients.All.UpdateData();
    }

    public async Task DeleteMessageById(string messageId)
    {
        await Clients.All.DeleteMessageReceived(messageId);
    }
    public async Task UserTyping(bool isTyping)
        => await Clients.Others.UserTyping(new ActorAction(Username, isTyping));
        
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