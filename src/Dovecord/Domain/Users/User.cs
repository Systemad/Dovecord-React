using System.Runtime.Serialization;
using Dovecord.Domain.Messages;
using Newtonsoft.Json;

namespace Dovecord.Domain.Users;
// TODO: Populate with user info 

public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public bool? IsOnline { get; set; }
    public bool? Bot { get; set; }
    public bool? System { get; set; }
    public bool? AccentColor { get; set; }
    //[JsonIgnore]
    //[IgnoreDataMember]
    //public ICollection<ChannelMessage> SentMessages { get; set; }
}