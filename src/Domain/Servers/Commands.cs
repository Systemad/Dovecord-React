using Domain.Channels;
using Domain.Channels.Dto;

namespace Domain.Servers;

public record CreateServerCommand(Guid ServerId, string Name, Guid InvokerUserId) : IEvent;

public record AddChannelCommand(Channel Channel, Guid ServerId, Guid InvokerUserId) : IEvent;

public record DeleteChannelCommand(Guid ChannelId, Guid ServerId, Guid InvokerUserId) : IEvent;

public record AddUserCommand(Guid ServerId, Guid InvokerUserId) : IEvent;
public record RemoveUserCommand(Guid ServerId, Guid InvokerUserId) : IEvent;