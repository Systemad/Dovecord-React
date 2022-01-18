using System.Runtime.Serialization;
using Dovecord.Domain.Messages;
using Newtonsoft.Json;

namespace Dovecord.Domain.Channels;

public class Channel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<ChannelMessage> ChannelMessages { get; set; }
}