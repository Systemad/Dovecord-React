using Orleans;

namespace Dovecord.Domain.Users;

public interface IUserGrain : IGrainWithGuidKey
{
    Task SetInitialServerGuid(List<Guid> serverGuids);
    Task<PresenceStatus> GetCurrentUserStatus();
    Task SetUserStatus(PresenceStatus status);
    Task JoinServer(Guid serverId);
    Task LeaveServer(Guid serverId);
    //Task BatchJoin(List<Guid> servers);
    //Task BatchLeave(List<Guid> servers);
}