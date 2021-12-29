using WebUI.Domain.Channels;
using WebUI.Domain.Users;

namespace WebUI.Domain.Messages;

public class ChannelMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }
        
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}