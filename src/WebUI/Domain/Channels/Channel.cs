using System.Runtime.Serialization;
using Newtonsoft.Json;
using WebUI.Domain.Messages;

namespace WebUI.Domain.Channels;

public class Channel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<ChannelMessage> ChannelMessages { get; set; }
}