using Infrastructure.Dtos.User;
using WebUI.Domain.Entities;
using WebUI.Dtos.Message;

namespace WebUI.SignalR;

public interface IChatClient
{
    Task SendUserList(List<UserDto> users);

    Task UserTyping(ActorAction action);

    Task MessageReceived(ChannelMessageDto message);

    Task DeleteMessageReceived(string id);

    Task AddToChannel(Guid channelId, Guid userId);
}