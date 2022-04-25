using Dovecord.Domain.Servers;
using Dovecord.Orleans.User;

namespace Dovecord.Domain.Users;
// TODO: Populate with user info 

public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public bool Bot { get; set; }
    public bool? System { get; set; }
    public bool? AccentColor { get; set; }
    public DateTime LastOnline { get; set; }
    public Guid UserSettingsId { get; set; }
    public UserSettings UserSettings { get; set; }
    public ICollection<Server>? Servers { get; set; }
}

public class UserSettings
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Bio { get; set; }
    //public string? Locale { get; set; }
    public PresenceStatus PresenceStatus { get; set; }
    public CustomStatus CustomStatus { get; set; }
    //public List<Guid>? GuildPositions { get; set; }
}