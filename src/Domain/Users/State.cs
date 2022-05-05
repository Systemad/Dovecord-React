using Domain.Servers;

namespace Domain.Users;

public partial class UserState
{
    public Guid Id { get; set; }
    public bool Created { get; set; }
    public string Name { get; set; }
    public PresenceStatus PresenceStatus { get; set; }
    public List<Guid> Servers { get; set; }
    public List<Guid> PrivateMessages { get; set; }

    public UserState()
    {
        PrivateMessages = new List<Guid>();
        Name = string.Empty;
        Servers = new List<Guid>();
        PresenceStatus = PresenceStatus.Online;
    }

    public UserState(
        Guid id,
        bool created,
        string name,
        List<Guid> servers,
        PresenceStatus presenceStatus,
        List<Guid> privateMessages)
    {
        Id = id;
        Created = created;
        Name = name;
        Servers = servers;
        PresenceStatus = presenceStatus;
        PrivateMessages = privateMessages;
    }

    public void Apply(UserCreatedEvent userCreatedEvent)
    {
        Id = userCreatedEvent.Id;
        Name = userCreatedEvent.Name;
    }
    
    public void Apply(ServerJoinedEvent serverJoinedEvent) => Servers.Add(serverJoinedEvent.ServerId);
    public void Apply(ServerLeftEvent serverLeftEvent) => Servers.Remove(serverLeftEvent.ServerId);

    public void Apply(UserStatusChangedEvent userStatusChangedEvent) =>
        PresenceStatus = userStatusChangedEvent.PresenceStatus;
}