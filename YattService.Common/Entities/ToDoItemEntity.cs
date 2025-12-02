namespace YattService.Common.Entities
{
    public class ToDoItemEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public UserEntity User { get; set; } = default!;
    }
}
