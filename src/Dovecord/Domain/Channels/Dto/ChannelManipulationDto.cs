namespace Dovecord.Domain.Channels.Dto;

public class ChannelManipulationDto
{
    public string? Name { get; set; }
    public int? Type { get; set; }
    // 0 =  Text, 1 = DM
    public string? Topic { get; set; }
    // IF DM, leave empty
    public Guid? ServerId { get; set; }
}