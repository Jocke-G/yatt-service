namespace YattService.Common.Entities
{
    public class UserEntity
    {
        public required string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public List<ToDoItemEntity> ToDoItems { get; set; } = [];
    }
}
