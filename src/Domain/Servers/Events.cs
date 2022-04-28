namespace Domain.Servers;

public record ServerCreatedEvent(Guid ServerId, string Name, Guid InvokerUserId);
public record ChannelCreatedCEvent(Guid ChannelId, Guid ServerId, string Name);
public record UserAddedEvent(Guid ServerId, Guid UserId);
public record UserRemovedEvent(Guid ServerId, Guid UserId);