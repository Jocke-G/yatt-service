using Microsoft.EntityFrameworkCore;
using YattService.Common.Entities;
using YattService.Common.RepositoryInterfaces;

namespace YattService.Persistance.Repositories
{
    public class ToDoItemRepository(AppDbContext _context) : IToDoItemRepository
    {
        public async Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity)
        {
            entity = (await _context.TodoItems
                .AddAsync(entity))
                .Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ToDoItemEntity?> GetByIdAsync(Guid id)
        {
            return await _context.TodoItems
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ToDoItemEntity>> GetAllForUserAsync(string userId)
        {
            return await _context.TodoItems
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<ToDoItemEntity> UpdateAsync(ToDoItemEntity entity)
        {
            entity = _context.TodoItems.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(ToDoItemEntity entity)
        {
            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
