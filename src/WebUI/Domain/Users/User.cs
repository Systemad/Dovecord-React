using System.Runtime.Serialization;
using Newtonsoft.Json;
using WebUI.Domain.Messages;

namespace WebUI.Domain.Users;
// TODO: Populate with user info 

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool? IsOnline { get; set; }
    
    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<ChannelMessage> SentMessages { get; set; }
}