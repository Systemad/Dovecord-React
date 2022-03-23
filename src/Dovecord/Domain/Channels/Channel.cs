using System.Runtime.Serialization;
using Dovecord.Domain.Messages;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Users;
using Newtonsoft.Json;

namespace Dovecord.Domain.Channels;

public class Channel
{
    public Guid Id { get; set; }
    // 0 =  Server channel Text, 1 = DM
    public int Type { get; set; }
    public string Name { get; set; }
    public string? Topic { get; set; }
    
    // If DM or Group DM - Type 1 Ignore
    public Guid? ServerId { get; set; }
    public Server? Server { get; set; }
    
    public ICollection<ChannelMessage>? Messages { get; set; }
    // If DM, put both author and recipient. When querying go through all channels that has both members in    
    public List<User>? Recipients { get; set; }
}