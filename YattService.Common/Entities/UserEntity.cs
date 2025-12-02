namespace YattService.Common.Entities
{
    public class UserEntity
    {
        public string Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public List<ToDoItemEntity> ToDoItems { get; set; } = [];
    }
}
