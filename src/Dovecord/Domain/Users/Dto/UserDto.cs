using Dovecord.Orleans.User;

namespace Dovecord.Domain.Users.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public bool? IsOnline { get; set; }
    public bool Bot { get; set; }
    public bool? System { get; set; }
    public bool? AccentColor { get; set; }
    public DateTime LastOnline { get; set; }
}

public class UserSettingsDto
{
    //public string? Locale { get; set; }
    public string? Bio { get; set; }
    public PresenceStatus PresenceStatus { get; set; }
    public CustomStatus CustomStatus { get; set; }
    //public List<Guid>? GuildPositions { get; set; }
}