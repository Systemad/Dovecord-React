using WebUI.Domain.Messages;

namespace WebUI.Domain.Channels;

public class Channel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    public virtual ICollection<ChannelMessage> ChannelMessages { get; set; }
}