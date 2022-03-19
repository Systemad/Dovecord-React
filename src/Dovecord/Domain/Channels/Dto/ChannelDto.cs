using Dovecord.Domain.Users;
using Dovecord.Domain.Users.Dto;

namespace Dovecord.Domain.Channels.Dto;

public class ChannelDto
{
    public Guid Id { get; set; }
    // 0 =  Server channel Text, 1 = DM
    public int Type { get; set; }
    public string? Name { get; set; }
    public string? Topic { get; set; }
    
    // If DM or Group DM - Type 1 Ignore
    public Guid? ServerId { get; set; }

    // If DM, put both author and recipient. When querying go through all channels that has both members in    
    //public List<UserDto>? Recipients { get; set; }
}