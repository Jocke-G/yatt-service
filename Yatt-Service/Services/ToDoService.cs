using YattService.Common.Entities;
using YattService.Common.RepositoryInterfaces;

namespace Yatt_Service.Services
{
    public class ToDoService(IToDoItemRepository _repository)
    {
        public async Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<IEnumerable<ToDoItemEntity>> GetAllForUserAsync(string userId)
        {
            return await _repository.GetAllForUserAsync(userId);
        }

        public async Task<ToDoItemEntity?> GetByIdAsync(string userId, Guid id)
        {
            return await _repository.GetByIdAsync(userId, id);
        }

        public async Task UpdateAsync(ToDoItemEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string userId, Guid id)
        {
            await _repository.DeleteAsync(userId, id);
        }
    }
}
