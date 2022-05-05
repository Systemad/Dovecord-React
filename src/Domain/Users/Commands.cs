namespace Domain.Users;

public record CreateUserCommand(Guid UserId, string Name);

public record JoinServerCommand(Guid ServerId, Guid InvokerUserId);
public record LeaveServerCommand(Guid ServerId, Guid InvokerUserId);

public record ChangeStatusCommand(PresenceStatus PresenceStatus, Guid InvokerUserId);