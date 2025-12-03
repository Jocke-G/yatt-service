using System.Security.Claims;

namespace Yatt_Service.Controllers
{
    public static class ControllerUtils
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Authenticated user has no NameIdentifier claim.");
        }
    }
}
