using Domain.Channels.Dto;
using Domain.Messages.Dto;
using Domain.Servers.Dto;
using Domain.Users;
using Domain.Users.Dto;

namespace Dovecord.SignalR.Hubs;

public interface IBaseHub
{
    Task SendUserList(List<UserDto> users);
    //Task UpdateData();
    //Task UserTyping(ActorAction action);

    Task MessageReceived(ChannelMessageDto message);

    Task DeleteMessageReceived(string channelId, string messageId);
    Task ChannelCreated(ChannelDto channelDto);
    Task ChannelDeleted(ChannelDto channelDto);
    Task ServerAction(ServerDto serverDto);
    Task ServerLeft(ServerDto serverDto);

    Task UserStatusChange(Guid userId, PresenceStatus status);
}