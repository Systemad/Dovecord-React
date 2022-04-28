using System.Security.Claims;

namespace Dovecord.Extensions.Services;
public interface ICurrentUserService
{
    Guid UserId { get; }
    string Username { get; }
}
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public Guid UserId => Guid.Parse(_httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
    public string Username => _httpContext.HttpContext?.User?.Identity?.Name;
}