using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YattService.Common.Entities;
using YattService.Common.RepositoryInterfaces;

namespace YattService.Persistance.Repositories
{
    public class ToDoItemRepository(ILogger<ToDoItemRepository> logger, YattDbContext context) : IToDoItemRepository
    {
        private readonly ILogger<ToDoItemRepository> _logger = logger;
        private readonly YattDbContext _context = context;

        public async Task<IEnumerable<ToDoItemEntity>> GetAllForUserAsync(string userId)
        {
            _logger.LogDebug("Fetching all ToDo items for user {userId}", userId);
            return await _context.TodoItems
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<ToDoItemEntity?> GetByIdAsync(string userId, Guid id)
        {
            _logger.LogDebug("Fetching ToDo item with Id {id} for user {userId}", id, userId);
            return await _context.TodoItems
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);
        }

        public async Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity)
        {
            _logger.LogDebug("Adding new ToDo item for user {userId}", entity.UserId);

            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(ToDoItemEntity entity)
        {
            _logger.LogDebug("Updating ToDo item with Id {id} for user {userId}", entity.Id, entity.UserId);

            var res = _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId, Guid id)
        {
            _logger.LogDebug("Deleting ToDo item with Id {id} for user {userId}", id, userId);

            var entity = await GetByIdAsync(userId, id);
            if (entity != null)
            {
                _context.TodoItems.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
