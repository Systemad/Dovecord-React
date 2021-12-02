using Domain.Entities;
using Domain.Messages;

namespace Domain.Channels;

public class Channel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<ChannelMessage> ChannelMessages { get; set; }
}