using System.Security.Claims;

namespace Yatt_Service.Auth
{
    internal class UserContext(IHttpContextAccessor accessor) : IUserContext
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        public string UserId =>
            _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new InvalidOperationException("No user id");
    }
}
