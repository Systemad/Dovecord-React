using Dovecord.Domain.Channels;
using Dovecord.Domain.Users;

namespace Dovecord.Domain.Servers;

public class Server
{
    public Guid Id { get; set; } = new();
    public string Name { get; set; }
    public string? IconUrl { get; set; }
    
    public List<Channel>? Channels { get; set; }
    public List<User>? Members { get; set; }
}