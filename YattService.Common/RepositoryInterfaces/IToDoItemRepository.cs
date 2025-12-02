using YattService.Common.Entities;

namespace YattService.Common.RepositoryInterfaces
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItemEntity>> GetAllForUserAsync(string userId);
        Task<ToDoItemEntity?> GetByIdAsync(string userId, Guid id);
        Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity);
        Task UpdateAsync(ToDoItemEntity entity);
        Task DeleteAsync(string userId, Guid id);
    }
}
