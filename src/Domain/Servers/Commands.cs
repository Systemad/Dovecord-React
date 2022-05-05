using Domain.Channels;
using Domain.Channels.Dto;

namespace Domain.Servers;

public record CreateServerCommand(Guid ServerId, string Name, Guid InvokerUserId);

public record AddChannelCommand(ChannelDto Channel);
public record DeleteChannelCommand(Guid ChannelId, Guid ServerId);

public record AddUserCommand(Guid ServerId, Guid UserId);
public record RemoveUserCommand(Guid ServerId, Guid UserId);