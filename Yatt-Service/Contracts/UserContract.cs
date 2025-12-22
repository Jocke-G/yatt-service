
namespace Yatt_Service.Contracts
{
    public class UserContract
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
