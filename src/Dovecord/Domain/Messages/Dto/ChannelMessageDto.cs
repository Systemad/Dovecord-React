using Dovecord.Domain.Users;

namespace Dovecord.Domain.Messages.Dto;

public class ChannelMessageDto
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? Content { get; set; }
    public User Author { get; set; }
    public Guid ChannelId { get; set; }
    public Guid? ServerId { get; set; }
}