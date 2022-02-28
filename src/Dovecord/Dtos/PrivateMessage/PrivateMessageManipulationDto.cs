namespace Dovecord.Dtos.PrivateMessage;

public class PrivateMessageManipulationDto
{
    public string? Content { get; set; }
    public Guid ReceiverId { get; set; }
}