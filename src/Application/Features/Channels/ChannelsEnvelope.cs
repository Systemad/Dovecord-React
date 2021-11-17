using Domain.Entities;

namespace Application.Features.Channels;

public class ChannelsEnvelope
{
    public List<TextChannel> TextChannels { get; set; } = new();
    public int TextChannelsCount { get; set; }
}