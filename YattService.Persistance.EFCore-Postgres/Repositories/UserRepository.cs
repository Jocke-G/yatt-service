using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YattService.Common.RepositoryInterfaces;

namespace YattService.Persistance.Repositories
{
    public class UserRepository(ILogger<UserRepository> logger, YattDbContext context) : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger = logger;
        private readonly YattDbContext _context = context;

        public async Task EnsureUserExistsAsync(string userId)
        {
            _logger.LogDebug("Ensuring user {userId} exists", userId);

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO users (id, created_at)
                VALUES ({userId}, now())
                ON CONFLICT (id) DO NOTHING
            ");
        }

        public async Task UpdateLastLoginIfOldAsync(string userId)
        {
            _logger.LogDebug("Updating last login for user {userId}", userId);

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                UPDATE users
                SET last_login = now()
                WHERE id = {userId}
                  AND (last_login IS NULL 
                    OR last_login < now() - interval '5 minutes');
            ");
        }
    }
}
