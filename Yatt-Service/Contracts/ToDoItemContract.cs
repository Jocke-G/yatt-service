namespace Yatt_Service.Contracts
{
    public class ToDoItemContract
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
