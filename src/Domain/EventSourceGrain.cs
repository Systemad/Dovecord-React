using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain;

public abstract class EventSourceGrain<TState>: JournaledGrain<TState> where TState : class, new()
{
    private IAsyncStream<object> _stream = null!;
    private readonly string _namespace;
    private readonly string _memoryStream;
    private IStreamProvider? _streamProvider;

    protected EventSourceGrain(string memoryStream, string ns)
    {
       _memoryStream = memoryStream;
       _namespace = ns;
    }

    public override Task OnActivateAsync()
    {
        _streamProvider = GetStreamProvider(_memoryStream);
        _stream = _streamProvider.GetStream<object>(this.GetPrimaryKey(), _namespace);
        Console.WriteLine("EventSourceGrain activated");
        return base.OnActivateAsync();
    }

    protected async Task PublishEventAsync(object evt)
    {
        RaiseEvent(evt);
        Console.WriteLine("PublishEventAsync Raised");
        await _stream.OnNextAsync(evt);
    }
    
    protected async Task PublishErrorAsync(object evt)
    {
        RaiseEvent(evt);
        Console.WriteLine("PublishErrorAsync Raised");
        await _stream.OnNextAsync(evt);
    }
}