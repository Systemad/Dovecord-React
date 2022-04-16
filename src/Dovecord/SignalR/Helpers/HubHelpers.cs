using System.Text.Json;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Messages.Dto;
using Dovecord.Domain.Servers.Dto;
using Dovecord.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Dovecord.SignalR.Helpers;

public static class HubHelpers
{
    public static async Task SendHubData(string dataJson, IHubContext<BaseHub, IBaseHub> hubContext)
    {
        var data = JsonSerializer.Deserialize<PushData<object>>(dataJson);
        switch (data._type)
        {
            case PushType.ServerCreateChannel:
                break;
            case PushType.ServerDeleteChannel:
                break;
            case PushType.ServerEditChannel:
                break;
            case PushType.UserJoinServer:
                break;
            case PushType.UserLeaveServer:
                break;

            case PushType.Login:
                break;
            case PushType.Logout:
                break;
            default:
                break;
        }
    }
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
        //await hubContext.Groups.AddToGroupAsync(hubContext., serverDto.Id.ToString());
        await hubContext.Clients.Group(userId).ServerAction(serverDto);
    }
}