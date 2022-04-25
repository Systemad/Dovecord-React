using Dovecord.Orleans.User;

namespace Dovecord.Domain.Users.Dto;

public class UserSettingsManipulationDto
{
    public string? Bio { get; set; }
    public PresenceStatus? PresenceStatus { get; set; }
    public CustomStatus? CustomStatus { get; set; }
}