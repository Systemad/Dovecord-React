using Dovecord.Domain.Channels;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Users;
using Dovecord.Domain.Users.Dto;

namespace Dovecord.Domain.Servers.Dto;

public class ServerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? IconUrl { get; set; }
    public Guid OwnerUserId { get; set; }
    
    //public ICollection<ChannelDto> Channels { get; set; }
    //public ICollection<UserDto> Members { get; set; }
}