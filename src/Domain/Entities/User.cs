namespace Domain.Entities;
// TODO: Populate with user info 

public class User
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public bool? IsOnline { get; set; }
    
    public virtual ICollection<ChannelMessage> SentMessages { get; set; }
}