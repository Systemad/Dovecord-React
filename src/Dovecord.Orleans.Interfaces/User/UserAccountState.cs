using Dovecord.Domain.Users;
using Dovecord.Domain.Users.Dto;

namespace Dovecord.Orleans.Interfaces.User;

[Serializable]
public record UserAccountState
{
    public bool Created { get; set; } = false;
    public UserDto User { get; set; } = new();
    public PresenceStatus PresenceStatus { get; set; }
    public List<Guid> Servers { get; set; } = new();
}