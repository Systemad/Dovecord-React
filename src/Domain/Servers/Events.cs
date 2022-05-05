using Domain.Channels;
using Domain.Channels.Dto;
using Domain.Servers.Dto;

namespace Domain.Servers;

public record ServerCreatedEvent(Server Server);
public record ChannelAddedEvent(ChannelDto Channel);
public record ChannelRemovedEvent(Guid ServerId, Guid ChannelId);
public record UserAddedEvent(Guid ServerId, Guid UserId);
public record UserRemovedEvent(Guid ServerId, Guid UserId);