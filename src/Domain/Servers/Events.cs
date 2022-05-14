using Domain.Channels;
using Domain.Channels.Dto;
using Domain.Servers.Dto;

namespace Domain.Servers;

public record ServerCreatedEvent(Server Server, Guid InvokerUserId);

public record ChannelAddedEvent(Channel Channel, Guid InvokerUserId);

public record ChannelRemovedEvent(Guid ServerId, Guid ChannelId, Guid InvokerUserId);

public record UserAddedEvent(Guid ServerId, Guid InvokerUserId);
public record UserRemovedEvent(Guid ServerId, Guid InvokerUserId);