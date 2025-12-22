using YattService.Common.Entities;

namespace YattService.Common.RepositoryInterfaces
{
    public interface IToDoItemRepository
    {
        Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity);
        Task<ToDoItemEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<ToDoItemEntity>> GetAllForUserAsync(string userId);
        Task<ToDoItemEntity> UpdateAsync(ToDoItemEntity entity);
        Task DeleteAsync(ToDoItemEntity entity);
    }
}
