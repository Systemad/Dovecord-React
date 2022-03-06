using Dovecord.Domain.Channels;
using Dovecord.Domain.Users;

namespace Dovecord.Domain.Servers.Dto;

public class ServerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Topic { get; set; }
    public Guid OwnerUserId { get; set; }
    public List<Channel>? Channels { get; set; }
    public List<User>? Members { get; set; }
}