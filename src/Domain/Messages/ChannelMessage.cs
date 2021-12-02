using Domain.Channels;
using Domain.Entities;
using Domain.Users;

namespace Domain.Messages;

public class ChannelMessage : BaseMessageEntity
{
    public string? Content { get; set; }
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }
        
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}