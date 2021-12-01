namespace Domain.Entities;

public class ChannelMessage
{
    public Guid ChannelMessageId { get; set; }
    public string? Content { get; set; }
    public DateOnly? CreatedAt { get; set; }
    public bool? IsEdit { get; set; }

    public Guid ChannelForeignKey { get; set; }
    public virtual Channel Channel { get; set; }
        
    public Guid UserForeignKey { get; set; }
    public virtual User User { get; set; }
}