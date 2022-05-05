namespace Domain.Users;

public record UserCreatedEvent(Guid Id, string Name);
    
public record ServerJoinedEvent(Guid ServerId, Guid InvokerUserId);
public record ServerLeftEvent(Guid ServerId, Guid InvokerUserId);

public record UserStatusChangedEvent(PresenceStatus PresenceStatus, Guid InvokerUserId);