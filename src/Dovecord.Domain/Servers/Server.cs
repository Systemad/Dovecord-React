namespace Dovecord.Domain.Servers;

public class Server
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? IconUrl { get; set; }
    public Guid OwnerUserId { get; set; }
    
    public ICollection<Channels.Channel>? Channels { get; set; }
    public ICollection<Users.User>? Members { get; set; }
}