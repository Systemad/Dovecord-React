using Dovecord.Domain.Entities;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.PrivateMessage;
using Dovecord.Dtos.User;

namespace Dovecord.SignalR;

public interface IChatClient
{
    Task SendUserList(List<UserDto> users);
    Task UpdateData();
    Task UserTyping(ActorAction action);

    Task MessageReceived(ChannelMessageDto message);
    Task PrivateMessageReceived(PrivateMessageDto message);

    Task DeleteMessageReceived(string channelId, string messageId);

    Task AddToChannel(Guid channelId, Guid userId);
}