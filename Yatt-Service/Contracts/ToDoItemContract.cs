namespace Yatt_Service.Contracts
{
    public class ToDoItemContract
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
