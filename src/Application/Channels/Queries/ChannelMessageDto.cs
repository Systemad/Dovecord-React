using Domain.Entities;

namespace Application.Channels.Queries;

public class ChannelMessageDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsEdit { get; set; }
    public string? Username { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid ChannelId { get; set; }
    public TextChannel Channel { get; set; }
}