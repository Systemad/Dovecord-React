using Dovecord.Domain.Messages.Dto;
using Dovecord.Domain.Users.Dto;

namespace Dovecord.SignalR.Hubs;

public interface IBaseHub
{
    Task SendUserList(List<UserDto> users);
    //Task UpdateData();
    //Task UserTyping(ActorAction action);

    Task MessageReceived(ChannelMessageDto message);

    Task DeleteMessageReceived(string channelId, string messageId);
}