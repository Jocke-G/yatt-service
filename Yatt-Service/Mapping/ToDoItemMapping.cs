using Yatt_Service.Contracts;
using Yatt_Service.Entities;

namespace Yatt_Service.Mapping
{
    public static class ToDoItemMapping
    {
        public static IEnumerable<ToDoItemContract> ToContracts(this IEnumerable<ToDoItemEntity> entities) => entities.Select(e => e.ToContract());

        public static ToDoItemContract ToContract(this ToDoItemEntity entity) => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            IsDone = entity.IsDone
        };

        public static ToDoItemEntity ToEntity(this ToDoItemContract contract) => new()
        {
            Id = contract.Id,
            Title = contract.Title,
            IsDone = contract.IsDone
        };

        public static ToDoItemEntity PatchFromContract(this ToDoItemEntity entity, ToDoItemContract contract)
        {
            entity.Title = contract.Title;
            entity.IsDone = contract.IsDone;
            return entity;
        }
    }
}
