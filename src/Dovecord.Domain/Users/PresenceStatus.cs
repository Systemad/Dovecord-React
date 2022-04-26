namespace Dovecord.Domain.Users;

public enum PresenceStatus
{
    Offline = 0,
    Away = 1,
    Online = 2,
    Invisible = 3
}

public class CustomStatus
{
    public Guid Id { get; set; }
    public string? NowPlaying { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }
}