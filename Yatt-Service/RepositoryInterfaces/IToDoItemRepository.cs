using Yatt_Service.Entities;

namespace Yatt_Service.RepositoryInterfaces
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItemEntity>> GetAllAsync();
        Task<ToDoItemEntity?> GetByIdAsync(int id);
        Task<ToDoItemEntity> AddAsync(ToDoItemEntity entity);
        Task UpdateAsync(ToDoItemEntity entity);
        Task DeleteAsync(int id);
    }
}
