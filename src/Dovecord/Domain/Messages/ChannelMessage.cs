using Dovecord.Domain.Channels;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Users;

namespace Dovecord.Domain.Messages;

public class ChannelMessage
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; }
        
    public Guid? ServerId { get; set; }
    public Server? Server { get; set; }

    public Guid? AuthorId { get; set; }
    public User? Author { get; set; }
}