namespace Dovecord.Dtos.PrivateMessage;

public class PrivateMessageDto
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? Content { get; set; }
    public Guid ReceiverUserId { get; set; }
    public Guid UserId { get; set; }
}