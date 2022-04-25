using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;

namespace Dovecord.Orleans.User;

[Serializable]
public record UserAccountState
{
    public bool Created { get; set; } = false;
    public UserDto User { get; set; } = new();
    public PresenceStatus PresenceStatus { get; set; }
    public List<Guid> Servers { get; set; } = new();
}