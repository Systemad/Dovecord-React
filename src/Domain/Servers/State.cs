namespace Domain.Servers;

public partial class ServerState
{
    public Guid Id { get; set; }
    public bool Created { get; set; }
    public string Name { get; set; }
    public List<Guid> Users { get; set; }
    public List<Guid> Channels { get; set; }

    public ServerState()
    {
        Name = string.Empty;
        Users = new List<Guid>();
        Channels = new List<Guid>();
    }

    public void Apply(ServerCreatedEvent serverCreatedEvent)
    {
        Id = serverCreatedEvent.ServerId;
        Name = serverCreatedEvent.Name;
    }

    public void Apply(UserAddedEvent userAddedEvent) => Users.Add(userAddedEvent.UserId);
    public void Apply(UserRemovedEvent userRemovedEvent) => Users.Remove(userRemovedEvent.UserId);
}