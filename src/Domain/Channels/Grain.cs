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

public class ChannelGrain : JournaledGrain<ChannelState>, IChannelGrain
{
    private IAsyncStream<object> _stream = null!;

    public override Task OnActivateAsync()
    {
        var streamProvider = GetStreamProvider(Constants.InMemoryStream);
        _stream = streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace);
        return base.OnActivateAsync();
    }

    public async Task CreateAsync(CreateChannelCommand command)
    {
        var channelExist = State.Created;
        if (channelExist)
            return; // Send error event
        
        var newChannel = new Channel
        {
            Id = command.ChannelId,
            Type = command.Type,
            Name = command.Name,
            Topic = command.Topic,
            ServerId = command.ServerId,
        };
        var evt = new ChannelCreatedEvent(newChannel);
        RaiseEvent(evt);
        await _stream.OnNextAsync(evt);
    }

    public Task AddMessageAsync(AddMessageCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChannelExist() => Task.FromResult(State.Created);
}