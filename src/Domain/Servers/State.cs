namespace Domain.Servers;

public partial class ServerState
{
    public Guid Id { get; set; }
    public bool Created { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public List<Guid> Users { get; set; }
    public List<Guid> Channels { get; set; }

    public ServerState()
    {
        Name = string.Empty;
        Users = new List<Guid>();
        Channels = new List<Guid>();
    }

    public void Apply(ServerCreatedEvent evt)
    {
        Id = evt.Server.Id;
        Name = evt.Server.Name;
        OwnerId = evt.Server.OwnerUserId;
    }
    
    public void Apply(ChannelAddedEvent evt) => Channels.Add(evt.Channel.Id);
    public void Apply(ChannelRemovedEvent evt) => Channels.Remove(evt.ChannelId);
    
    public void Apply(UserAddedEvent evt) => Users.Add(evt.InvokerUserId);
    public void Apply(UserRemovedEvent evt) => Users.Remove(evt.InvokerUserId);
}