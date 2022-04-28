namespace Domain.Users;

[Serializable]
public record UserAccountState
{
    public bool Created { get; set; }
    public string Username { get; set; }
    public PresenceStatus PresenceStatus { get; set; }
    public List<Guid> Servers { get; set; } = new();
}