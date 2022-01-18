using System.Security.Claims;

namespace Dovecord.Extensions.Services;
public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
}
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string? UserId => _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Username => _httpContext.HttpContext?.User?.Identity?.Name;
}