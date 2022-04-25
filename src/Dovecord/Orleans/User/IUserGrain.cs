using Orleans;

namespace Dovecord.Orleans.User;

public interface IUserGrain : IGrainWithStringKey
{
    Task<PresenceStatus> GetCurrentUserStatus();
    Task SetUserStatus(PresenceStatus status);
    Task JoinServer(Guid serverId);
    Task LeaveServer(Guid serverId);
    //Task BatchJoin(List<Guid> servers);
    //Task BatchLeave(List<Guid> servers);
}