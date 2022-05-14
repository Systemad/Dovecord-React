namespace Domain;

public interface IEvent
{
    Guid ServerId { get; }
    Guid InvokerUserId { get; }
}