namespace Yatt_Service.Exceptions
{
    public class UserNotFoundException(string id) : NotFoundException($"User with Id: {id} not found.")
    {
    }
}
