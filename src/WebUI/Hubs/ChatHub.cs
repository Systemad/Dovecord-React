using System.Security.Claims;
using Dovecord.Server.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebUI.Hubs;

[Authorize]
[RequiredScope("API.Access")]
public class ChatHub : Hub<IChatClient>
{
    private IUserService _userService;
    public ChatHub(IUserService userService)
    {
        _userService = userService;
    }

    string Username => Context?.User?.Identity?.Name ?? "Unknown";
    Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        var exist = await _userService.CheckIfUserExistAsync(UserId);
        if (!exist)
            await _userService.CreateUserAsync(UserId, Username);
            
        await _userService.UserLoggedOnAsync(UserId);
        await Clients.All.SendUserList(await _userService.GetUsersAsync());
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        await _userService.UserLoggedOffAsync(UserId);
        await Clients.All.SendUserList(await _userService.GetUsersAsync());
    }

    public async Task PostMessage(ChannelMessage message, Guid channelId)
    {
        await Clients.Group(channelId.ToString()).MessageReceived(message);
    }

    public async Task DeleteMessageById(string messageId)
    {
        await Clients.All.DeleteMessageReceived(messageId);
    }
    public async Task UserTyping(bool isTyping)
        => await Clients.Others.UserTyping(new ActorAction(Username, isTyping));
        
    public async Task JoinChannelById(Guid channelId)
    {
        Console.Write($"Joined channel - {channelId.ToString()}");
        await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
    }
        
    public async Task RemoveChannelById(Guid channelId)
    {
        Console.Write($"Left channel - {channelId.ToString()}");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId.ToString());
    }
}