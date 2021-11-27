using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Channel
{
    /*
    public Channel()
    {
        ChannelMessages = new Collection<ChannelMessage>();
    }
    */
        
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}