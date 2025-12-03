namespace YattService.Common.Entities
{
    public class ToDoItemEntity
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public required string Title { get; set; }
        public bool IsDone { get; set; }
        public UserEntity? User { get; set; }
    }
}
