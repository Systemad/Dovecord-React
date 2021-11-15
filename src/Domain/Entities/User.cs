namespace Domain.Entities;
// TODO: Populate with user info 

public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public bool Online { get; set; }
    
    public Guid MessageId { get; set; }
    public ChannelMessage Message { get; set; } = null!;

    public IList<ChannelMessage> Messages { get; set; } = new List<ChannelMessage>();
}