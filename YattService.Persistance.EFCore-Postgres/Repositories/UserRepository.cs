using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YattService.Common.Entities;
using YattService.Common.RepositoryInterfaces;

namespace YattService.Persistance.Repositories
{
    public class UserRepository(ILogger<UserRepository> _logger, AppDbContext _context) : IUserRepository
    {
        public async Task EnsureUserExistsAsync(string id)
        {
            var res = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO users (id, created_at)
                VALUES ({id}, now())
                ON CONFLICT (id) DO NOTHING
            ");

            if (res == 1)
            {
                _logger.LogInformation("Created new user {userId}", id);
            }
        }

        public async Task UpdateLastLoginIfOldAsync(string id)
        {
            var res = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                UPDATE users
                SET last_login = now()
                WHERE id = {id}
                  AND (last_login IS NULL 
                    OR last_login < now() - interval '5 minutes');
            ");

            if (res == 1)
            {
                _logger.LogInformation("Updated last login for user {userId}", id);
            }
        }

        public async Task<UserEntity?> GetAsync(string id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
