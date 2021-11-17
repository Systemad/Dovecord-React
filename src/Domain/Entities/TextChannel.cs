using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class TextChannel
{
    public TextChannel()
    {
        ChannelMessages = new Collection<ChannelMessage>();
    }
        
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<ChannelMessage> ChannelMessages { get; set; }
}