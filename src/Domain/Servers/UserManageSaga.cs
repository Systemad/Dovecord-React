using Domain.Users;
using Orleans;
using Orleans.Streams;

namespace Domain.Servers;

[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class UserManageSaga : Grain, ISubscriberGrain
{
    private StreamSubscriptionHandle<object>? _sub;
    private IStreamProvider? StreamProvider;
    
    public UserManageSaga()
    {
    }
    
    public override async Task OnActivateAsync()
    {
        Console.WriteLine("UserSaga activated");
        StreamProvider = GetStreamProvider(Constants.InMemoryStream);

        _sub = await StreamProvider
            .GetStream<object>(this.GetPrimaryKey(), Constants.UserNamespace)
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
            case UserAddedEvent obj:
                return await Handle(obj);
            default:
                return false;
        }
    }
    
    private async Task<bool> Handle(UserAddedEvent evt)
    {
        var userGrain = GrainFactory.GetGrain<IUserGrain>(evt.UserId);
        var joinServerCommand = new JoinServerCommand(evt.ServerId, evt.UserId);

        await userGrain.JoinServerAsync(joinServerCommand);
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
}