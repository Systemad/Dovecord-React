using Dovecord.Domain.Users;

namespace Dovecord.Domain.PrivateMessage;

public class PrivateMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public Guid ReceiverUserId { get; set; }
    public virtual User ReceiverUser { get; set; }
        
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}