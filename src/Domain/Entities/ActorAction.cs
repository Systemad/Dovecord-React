namespace Domain.Entities
{
    public record ActorAction(string User, bool IsTyping) : Actor(User);
}
