
using YattService.Common.Entities;
using YattService.Common.RepositoryInterfaces;

namespace Yatt_Service.Services
{
    public class UserService(IUserRepository repository)
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UserEntity?> GetUser(string userId)
        {
            return await _repository.GetUser(userId);
        }
    }
}
