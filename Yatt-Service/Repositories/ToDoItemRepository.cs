using Microsoft.EntityFrameworkCore;
using Yatt_Service.Entities;
using Yatt_Service.RepositoryInterfaces;

namespace Yatt_Service.Repositories
{
    public class ToDoItemRepository(ILogger<ToDoItemRepository> logger, YattDbContext context) : IToDoItemRepository
    {
        private readonly ILogger<ToDoItemRepository> _logger = logger;
        private readonly YattDbContext _context = context;

        public async Task<IEnumerable<ToDoItemEntity>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<ToDoItemEntity?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity)
        {
            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(ToDoItemEntity entity)
        {
            var res = _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.TodoItems.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
