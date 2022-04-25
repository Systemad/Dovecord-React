using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Extensions.Services;
using Dovecord.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Orleans;
using Orleans.Runtime;
using Serilog;

namespace Dovecord.Orleans.User;

/*
 * User Grain, one grain for each user, represented by GUID
 * which contains _currentStatus
 * and serverList (of IDS).
 * Check Chirper example
 * Also set UserDto as state?
 */
public class UserGrain : Grain, IUserGrain
{
    private readonly IPersistentState<UserAccountState> _state;
    private readonly IHubContext<BaseHub, IBaseHub> _hubContext;

    private string GrainKey => this.GetPrimaryKeyString();
    private static string GrainType = nameof(UserGrain);
    
    /*
    public UserGrain(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    */
    public UserGrain([PersistentState("user", "UserState")] IPersistentState<UserAccountState> state, IHubContext<BaseHub, IBaseHub> hubContext)
    {
        _state = state;
        _hubContext = hubContext;
    }
    
    public override Task OnActivateAsync()
    {
        Log.Information("{GrainType} {GrainKey} activated", GrainType, GrainKey);
        _state.State.Created = true;
        return Task.CompletedTask;
    }
    
    public Task<PresenceStatus> GetCurrentUserStatus() => Task.FromResult(_state.State.PresenceStatus);

    public async Task SetInitialServerGuid(List<Guid> serverGuids)
    {
        if (_state.State.Created)
            return;
        
        _state.State.Servers = serverGuids;
        await _state.WriteStateAsync();
    }
    public async Task SetUserStatus(PresenceStatus status)
    {
        _state.State.PresenceStatus = status;
        await _state.WriteStateAsync();
    }

    public Task<List<Guid>> GetCurrentServers() => Task.FromResult(_state.State.Servers);

    public async Task NewStatusAsync(PresenceStatus status, Guid userId)
    {
        foreach (var server in _state.State.Servers)
        {
            await _hubContext.Clients.Group(server.ToString()).UserStatusChange(userId, status);
        }
    }
    
    public Task JoinServer(Guid serverId)
    {
        if (!_state.State.Servers.Contains(serverId))
            _state.State.Servers.Add(serverId);
        return Task.CompletedTask;
    }

    public Task LeaveServer(Guid serverId)
    {
        if (_state.State.Servers.Contains(serverId))
            _state.State.Servers.Remove(serverId);
        return Task.CompletedTask;
    }
}