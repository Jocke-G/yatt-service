namespace Yatt_Service.Exceptions
{
    public class ToDoNotFoundException(Guid id) : NotFoundException($"ToDo with Id: {id} not found.")
    {
    }
}
