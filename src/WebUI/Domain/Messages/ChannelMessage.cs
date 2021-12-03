using WebUI.Domain.Channels;
using WebUI.Domain.Users;

namespace WebUI.Domain.Messages;

public class ChannelMessage : BaseMessageEntity
{
    public string? Content { get; set; }
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }
        
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}