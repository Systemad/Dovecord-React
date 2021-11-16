namespace Application.Channels.Queries;

public class TextChannelDto
{
    public TextChannelDto()
    {
        Messages = new List<ChannelMessageDto>();
    }
    
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public IList<ChannelMessageDto> Messages { get; set; }
}