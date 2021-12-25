using System.Security.Claims;

namespace WebUI.Extensions.Services;
public interface ICurrentUserService
{
    string? UserId { get; }
}
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    //public static string GetUserId(this HttpContext httpContext) => httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? UserId => _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}