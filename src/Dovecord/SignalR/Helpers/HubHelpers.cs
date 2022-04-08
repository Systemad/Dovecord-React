using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Messages.Dto;
using Dovecord.Domain.Servers.Dto;
using Dovecord.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Dovecord.SignalR.Helpers;

public static class HubHelpers
{
    // Switch case maybe?
    public static async Task SendMessageToTopic(ChannelMessageDto message, IHubContext<BaseHub, IBaseHub> hubContext)
    {
        var serverId = message.ServerId;
        if (serverId != null)
        {
            await hubContext.Clients.Groups(serverId.ToString()).MessageReceived(message);
        }
    }

    public static async Task SendChannelCreated(ChannelDto channelDto, IHubContext<BaseHub, IBaseHub> hubContext)
    {
        var serverId = channelDto.ServerId;
        if (serverId != null)
        {
            await hubContext.Clients.Groups(serverId.ToString()).ChannelCreated(channelDto);
        }
    }

    public static async Task JoinedServer(ServerDto serverDto, string userId, IHubContext<BaseHub, IBaseHub> hubContext)
    {
        await hubContext.Groups.AddToGroupAsync(userId, serverDto.Id.ToString());
        await hubContext.Clients.User(userId).ServerAction(serverDto);
    }
}