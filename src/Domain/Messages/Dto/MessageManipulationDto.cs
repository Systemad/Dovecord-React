namespace Domain.Messages.Dto;

public class MessageManipulationDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string? Content { get; set; }
    public int Type { get; set; }
    public Guid ChannelId { get; set; }
}