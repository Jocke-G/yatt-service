namespace Yatt_Service.Contracts
{
    public class ToDoItemContract
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
