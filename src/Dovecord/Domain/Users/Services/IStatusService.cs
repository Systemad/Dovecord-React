namespace Dovecord.Domain.Users.Services;

public interface IStatusService
{
    Task OnStartSession(Guid userId, string? username);
    Task OnStopSession(Guid userId);
}