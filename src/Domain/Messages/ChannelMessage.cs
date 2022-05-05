using Domain.Channels;
using Domain.Servers;
using Domain.Users;

namespace Domain.Messages;

public class ChannelMessage
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int Type { get; set; } // 0 Channel message, 1 PM
    
    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; }
        
    public Guid? ServerId { get; set; }
    public Server? Server { get; set; }

    public Guid AuthorId { get; set; }
    public User Author { get; set; }
}

public record CreateMessageModel(string Content,
    string CreatedBy, DateTime CreatedOn,
    bool IsEdit, DateTime LastModifiedOn,
    int Type, Guid ChannelId,
    Guid? ServerId, Guid AuthorId);