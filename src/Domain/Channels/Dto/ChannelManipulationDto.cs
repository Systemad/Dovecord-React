namespace Domain.Channels.Dto;

public class ChannelManipulationDto
{
    public string? Name { get; set; }
    //public int Type { get; set; }
    public string? Topic { get; set; }  // 0 =  Text Channel, 1 = DM
    //public Guid? ServerId { get; set; } // Leave empty if DM
}

public record CreateChannelModel(string Name, string Topic, int Type);