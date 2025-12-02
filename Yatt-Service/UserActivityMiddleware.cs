using System.Security.Claims;
using YattService.Common.RepositoryInterfaces;

namespace Yatt_Service
{
    public class UserActivityMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Authenticated user has no NameIdentifier claim.");
                await userRepository.EnsureUserExistsAsync(userId);
                await userRepository.UpdateLastLoginIfOldAsync(userId);
            }

            await _next(context);
        }
    }
}
