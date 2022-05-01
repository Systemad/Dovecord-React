using Application.Servers.Features;
using Domain;
using Domain.Servers;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Application.Servers;


[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class ServerSubscriber : Grain, ISubscriberGrain
{
    private StreamSubscriptionHandle<object>? _sub;
    private IStreamProvider? StreamProvider;
    private readonly IMediator _mediator;

    public ServerSubscriber(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override async Task OnActivateAsync()
    {
        Console.WriteLine("ServerSubscribe activated");
        var streamProvider = GetStreamProvider(Constants.InMemoryStream);
        var stream = streamProvider.GetStream<string>(this.GetPrimaryKey(), Constants.ServerNamespace);
        await stream.SubscribeAsync(HandleAsync);
        
        Console.WriteLine("ServerSubscribe: After stream");
        await base.OnActivateAsync();
    }
    
    public override async Task OnDeactivateAsync()
    {
        await _sub!.UnsubscribeAsync();
        await base.OnDeactivateAsync();
    }

    private async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
    {
        switch (evt)
        {
            case ServerCreatedEvent obj:
                return await Handle(obj);
            default:
                return false;
        }
    }

    private async Task<bool> Handle(ServerCreatedEvent evt)
    {
        var command = new AddServer.AddServerCommand(evt);
        await _mediator.Send(command);
        return true;
    }

}