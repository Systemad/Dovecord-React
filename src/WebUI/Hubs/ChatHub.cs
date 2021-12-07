using System.Security.Claims;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using WebUI.Domain.Messages;

namespace WebUI.Hubs;

[Authorize]
[RequiredScope("API.Access")]
public class ChatHub : Hub<IChatClient>
{
    private readonly IMediator _mediator;
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    string Username => Context?.User?.Identity?.Name ?? "Unknown";
    Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public override async Task OnConnectedAsync()
    {
        /*
        var usersCommand = new GetUserList.UserListQuery();
        var usersCommandResponse = await _mediator.Send(usersCommand);
        //var query = new GetUser.UserQuery(UserId);
        
        // TODO: Add Login
        var userCommand = new GetUser.UserQuery(UserId);
        var userCommandResponse = await _mediator.Send(userCommand);
        userCommandResponse.IsOnline = true;

        // Fix mapping ?
        var updateUser = new UpdateUser.UpdateUserCommand(userCommandResponse.Id, userCommandResponse);
        await _mediator.Send(updateUser);
        await Clients.All.SendUserList(usersCommandResponse);
        */
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        /*
        var usersCommand = new GetUserList.UserListQuery();
        var usersCommandResponse = await _mediator.Send(usersCommand);
        //var query = new GetUser.UserQuery(UserId);
        
        // TODO: Add Login
        var userCommand = new GetUser.UserQuery(UserId);
        var userCommandResponse = await _mediator.Send(userCommand);
        userCommandResponse.IsOnline = false;

        // Fix mapping 
        var updateUser = new UpdateUser.UpdateUserCommand(usersCommandResponse);
        await _mediator.Send(updateUser);
        */
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