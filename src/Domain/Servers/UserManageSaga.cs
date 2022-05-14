using Domain.Users;
using Orleans;
using Orleans.Streams;

namespace Domain.Servers;

[ImplicitStreamSubscription(Constants.ServerNamespace)]
public class UserManageSaga : SubscriberGrain
{
    public UserManageSaga() : base(Constants.InMemoryStream, Constants.ServerNamespace)
    {
    }
    
    public override async Task<bool> HandleAsync(object evt, StreamSequenceToken token)
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
        var userGrain = GrainFactory.GetGrain<IUserGrain>(evt.InvokerUserId);
        var joinServerCommand = new JoinServerCommand(evt.ServerId, evt.InvokerUserId);

        await userGrain.JoinServerAsync(joinServerCommand);
        // Send the server object to persistence store
        // SEND IT TO SIGNALR??
        // send it to next event
        return true;
    }
}