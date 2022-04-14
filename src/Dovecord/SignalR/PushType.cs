namespace Dovecord.SignalR;

public enum PushType : byte
{
    ServerCreateChannel = 0,
    ServerDeleteChannel = 1,
    ServerEditChannel = 2,
    //ServerSendMessageChannel = 3,
    //ServerMChannel
    //ServerSendMessageChannel
    
    UserJoinServer = 3,
    UserLeaveServer = 4,
    
    Login = 5,
    Logout = 6
}