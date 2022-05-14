using Orleans;
using Orleans.Streams;

namespace Domain;

public interface ISubscriberGrain : IGrainWithGuidKey
{
}

public abstract class SubscriberGrain : Grain, ISubscriberGrain
{
    private string _memorystream;
    private string _namespace;
    private StreamSubscriptionHandle<object>? _sub;
    protected IStreamProvider? StreamProvider;

    protected SubscriberGrain(string memorystream, string ns)
    {
        _memorystream = memorystream;
        _namespace = ns;
    }

    public override async Task OnActivateAsync()
    {
        StreamProvider = GetStreamProvider(_memorystream);
        _sub = await StreamProvider
            .GetStream<object>(this.GetPrimaryKey(), _namespace)
            .SubscribeAsync(HandleAsync);
        await base.OnActivateAsync();
    }

    public override async Task OnDeactivateAsync()
    {
        await _sub!.UnsubscribeAsync();
        await base.OnDeactivateAsync();
    }

    public abstract Task<bool> HandleAsync(object evt, StreamSequenceToken token);
}