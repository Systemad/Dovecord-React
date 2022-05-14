using Domain.Messages;
using Domain.Messages.Dto;

namespace Domain.Channels;

public record ChannelCreatedEvent(Channel Channel, Guid InvokerUserId);

public record ChannelDeletedEvent(Guid ChannelId, Guid InvokerUserId);

/*  Guid Id, Guid UserId, string Username, string Content, int Type, Guid ChannelId, Guid ServerId */
public record MessageAddedEvent(ChannelMessage Message, Guid InvokerUserId);

public record MessageDeletedEvent(Guid ChannelId, Guid MessageId, Guid InvokerUserId);