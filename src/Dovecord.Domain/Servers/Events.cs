namespace Dovecord.Domain.Servers;

// TODO: Remove mediatr, since this is essentially that, do logic in the handlers
public record ServerCreatedEvent(Guid ServerId, string Name);
public record ChannelCreatedCEvent(Guid ChannelId, Guid ServerId, string Name);
public record UserAddedEvent(Guid ServerId, Guid UserId);
public record UserRemovedEvent(Guid ServerId, Guid UserId);