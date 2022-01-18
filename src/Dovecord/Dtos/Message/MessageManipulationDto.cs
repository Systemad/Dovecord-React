namespace Dovecord.Dtos.Message;

public class MessageManipulationDto
{
    public string? Content { get; set; }
    public Guid ChannelId { get; set; }
}