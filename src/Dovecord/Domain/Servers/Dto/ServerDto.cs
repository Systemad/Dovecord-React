using Dovecord.Domain.Users;

namespace Dovecord.Domain.Servers.Dto;

public class ServerDto
{
    public Guid Id { get; set; }

    // 0 =  Server channel Text, 1 = DM
    public int Type { get; set; }
    public string? Name { get; set; }
    public string? Topic { get; set; }

    // If DM, put both author and recipient. When querying go through all channels that has both members in    
    //public List<User>? Recipients { get; set; }
}