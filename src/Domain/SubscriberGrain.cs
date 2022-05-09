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
    private IStreamProvider? _streamProvider;

    protected SubscriberGrain(string memorystream, string ns)
    {
        _memorystream = memorystream;
        _namespace = ns;
    }

    public override async Task OnActivateAsync()
    {
        Console.WriteLine("UserSubscriber activated");
        _streamProvider = GetStreamProvider(_memorystream);
        _sub = await _streamProvider
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