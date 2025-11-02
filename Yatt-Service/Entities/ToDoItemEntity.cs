using System.ComponentModel.DataAnnotations.Schema;

namespace Yatt_Service.Entities
{
    [Table("todo_items")]
    public class ToDoItemEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; } = string.Empty;
        [Column("is_done")]
        public bool IsDone { get; set; }
    }
}
