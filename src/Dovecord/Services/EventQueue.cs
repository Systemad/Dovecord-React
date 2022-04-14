using System.Threading.Channels;
using MediatR;

namespace Dovecord.Services;

public interface IEventQueue
{
    ValueTask Queue(IRequest queuedRequest);
    ValueTask<IRequest> DeQueue(CancellationToken cancellationToken);
}

public class EventQueue : IEventQueue
{
    private const int QueueCapacity = 100;
    private readonly Channel<IRequest> _queue;
    
    public EventQueue()
    {
        var options = new BoundedChannelOptions(QueueCapacity)
        {
            FullMode = BoundedChannelFullMode.DropOldest
        };
        _queue = Channel.CreateBounded<IRequest>(options);
    }

    public async ValueTask Queue(IRequest queuedRequest)
    {
        await _queue.Writer.WriteAsync(queuedRequest);
    }

    public async ValueTask<IRequest> DeQueue(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}

public interface IEnsureUserExistsRequest : IRequest
{
    Guid UserId { get; }
}