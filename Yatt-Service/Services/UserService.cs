
using Yatt_Service.Auth;
using Yatt_Service.Contracts;
using Yatt_Service.Exceptions;
using Yatt_Service.Mapping;
using YattService.Common.RepositoryInterfaces;

namespace Yatt_Service.Services
{
    public class UserService(ILogger<UserService> _logger, IUserContext _userContext, IUserRepository _userRepository)
    {
        public async Task<UserContract> GetUserAsync()
        {
            var userId = _userContext.UserId;
            _logger.LogDebug("Getting user with Id: {UserId}", userId);
            return (await _userRepository.GetAsync(_userContext.UserId)
                ?? throw new UserNotFoundException(userId))
                .ToContract();
        }
    }
}
