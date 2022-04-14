namespace Dovecord.SignalR;

public class PushData<T>
{
    public PushData(PushType type, T payload, string contextId)
    {
        _type = type;
        _payload = payload;
        _contextId = contextId;
    }
    public PushType _type;
    public T _payload;
    public string _contextId;
}