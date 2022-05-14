using Domain.Messages;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain.Channels;

public interface IChannelGrain : IGrainWithGuidKey
{
    Task CreateAsync(CreateChannelCommand command);
    Task AddMessageAsync(AddMessageCommand command);
    Task<bool> ChannelExist();
}

public class ChannelGrain : EventSourceGrain<ChannelState>, IChannelGrain
{
    public ChannelGrain() : base(Constants.InMemoryStream, Constants.ChannelNamespace){}

    public async Task CreateAsync(CreateChannelCommand command)
    {
        var channelExist = State.Created;

        var newChannel = new Channel
        {
            Id = command.ChannelId,
            Type = command.Type,
            Name = command.Name,
            Topic = command.Topic,
            ServerId = command.ServerId,
        };
        var evt = new ChannelCreatedEvent(newChannel, command.InvokerUserId);
        var task = channelExist ? PublishEventAsync(evt) : PublishErrorAsync(evt);
        await task;
    }

    public async Task AddMessageAsync(AddMessageCommand command)
    {
        var newMsg = new ChannelMessage
        {
            Id = command.MessageId,
            Content = command.Content,
            CreatedBy = command.CreatedBy,
            CreatedOn = command.CreatedOn,
            IsEdit = command.IsEdit,
            LastModifiedOn = command.LastModifiedOn,
            Type = command.Type,
            ChannelId = command.ChannelId,
            ServerId = command.ServerId,
            AuthorId = command.InvokerUserId,
        };
        var newEvent = new MessageAddedEvent(newMsg, command.InvokerUserId);
        await PublishEventAsync(newEvent);
    }

    public Task<bool> ChannelExist() => Task.FromResult(State.Created);
}