using System.Collections.ObjectModel;

namespace Domain.Entities;

public class TextChannel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public IList<ChannelMessage> Messages { get; set; } = new List<ChannelMessage>();
}