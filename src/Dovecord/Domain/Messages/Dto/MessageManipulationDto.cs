namespace Dovecord.Domain.Messages.Dto;

public class MessageManipulationDto
{
    public string? Content { get; set; }
    public Guid ChannelId { get; set; }
}