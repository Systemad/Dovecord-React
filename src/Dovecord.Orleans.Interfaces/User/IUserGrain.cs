using Dovecord.Domain.Users;
using Orleans;

namespace Dovecord.Orleans.Interfaces.User;

public interface IUserGrain : IGrainWithStringKey
{
    Task SetInitialServerGuid(List<Guid> serverGuids);
    Task<PresenceStatus> GetCurrentUserStatus();
    Task SetUserStatus(PresenceStatus status);
    Task JoinServer(Guid serverId);
    Task LeaveServer(Guid serverId);
    //Task BatchJoin(List<Guid> servers);
    //Task BatchLeave(List<Guid> servers);
}