namespace Domain.Entities;

public class Channel
{
    public Guid ChannelId { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<ChannelMessage> ChannelMessages { get; set; }
}