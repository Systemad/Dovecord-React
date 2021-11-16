namespace WebUI.Services;
using System.Security.Claims;

public static class CurrentUserService
{
    public static string GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}