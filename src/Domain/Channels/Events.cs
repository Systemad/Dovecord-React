using Domain.Messages;
using Domain.Messages.Dto;

namespace Domain.Channels;

public record ChannelCreatedEvent(Channel Channel);
public record ChannelDeletedEvent(Guid ChannelId);

/*  Guid Id, Guid UserId, string Username, string Content, int Type, Guid ChannelId, Guid ServerId */
public record MessageAddedEvent(ChannelMessage Message);

public record MessageDeletedEvent(Guid ChannelId, Guid MessageId);