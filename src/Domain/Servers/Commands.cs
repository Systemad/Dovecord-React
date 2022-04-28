namespace Domain.Servers;

public record CreateServerCommand(Guid ServerId, string Name, Guid InvokerUserId);
public record CreateChannelCommand(Guid ChannelId, Guid ServerId, string Name);
public record AddUserCommand(Guid ServerId, Guid UserId);
public record RemoveUserCommand(Guid ServerId, Guid UserId);