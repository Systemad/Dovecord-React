using Domain.Messages;

namespace Domain.Channels;

public record CreateChannelCommand(Guid ServerId, Guid ChannelId, string Name, string Topic, int Type);
public record DeleteChannelCommand(Guid ChannelId);

public record AddMessageCommand(Guid MessageId, string Content,
    string CreatedBy, DateTime CreatedOn,
    bool IsEdit, DateTime LastModifiedOn,
    int Type, Guid ChannelId,
    Guid? ServerId, Guid AuthorId);

public record DeleteMessageCommand(Guid ChannelId, Guid MessageId);