using Domain.Channels;
using Domain.Users;
using Orleans;

namespace Domain.Servers;

public class Server
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? IconUrl { get; set; }
    public Guid OwnerUserId { get; set; }
    
    public ICollection<Channel>? Channels { get; set; }
    public ICollection<User>? Members { get; set; }
}


public interface ISubscriberGrain : IGrainWithGuidKey
{
}