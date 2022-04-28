using Domain;
using Domain.Servers;
using Dovecord.Domain.Servers.Features;
using MediatR;
using Orleans;
using Orleans.Streams;

namespace Dovecord.Domain.Servers;

[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class ServerSubscriber : Grain, IGrainWithGuidKey
{
    private StreamSubscriptionHandle<object> _sub;
    private IAsyncStream<object> _stream = null!;
    private readonly IMediator _mediator;

    public ServerSubscriber(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override async Task OnActivateAsync()
    {
        var streamProvider = GetStreamProvider(Constants.ServerNamespace);
        _sub = await
            streamProvider.GetStream<object>(this.GetPrimaryKey(), Constants.ServerNamespace)
                .SubscribeAsync(HandleAsync);
            
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