using Yatt_Service.Auth;
using Yatt_Service.Contracts;
using Yatt_Service.Exceptions;
using Yatt_Service.Mapping;
using YattService.Common.RepositoryInterfaces;

namespace Yatt_Service.Services
{
    public class ToDoService(ILogger<UserService> _logger, IUserContext _userContext, IToDoItemRepository _repository)
    {
        public async Task<ToDoItemContract> AddAsync(ToDoItemContract contract)
        {
            var userId = _userContext.UserId;
            _logger.LogDebug("Adding ToDo for userId: {UserId}", userId);
            return (await _repository.AddAsync(contract.ToEntity(userId)))
                .ToContract();
        }

        public async Task<ToDoItemContract?> GetByIdAsync(Guid id)
        {
            var userId = _userContext.UserId;
            _logger.LogDebug("Retrieving ToDo with id: {ToDoId} for userId: {UserId}", id, userId);
            var entity = await _repository.GetByIdAsync(id) ?? throw new ToDoNotFoundException(id);

            if (entity.UserId != userId)
            {
                throw new ForbiddenException($"User with id {userId} is not authorized to access ToDo with id {id}.");
            }

            return entity.ToContract();
        }

        public async Task<IEnumerable<ToDoItemContract>> GetAllForUserAsync()
        {
            var userId = _userContext.UserId;
            _logger.LogDebug("Retrieving all ToDos for userId: {UserId}", userId);
            return (await _repository.GetAllForUserAsync(userId))
                .ToContracts();
        }


        public async Task<ToDoItemContract> UpdateAsync(Guid id, ToDoItemContract contract)
        {
            var userId = _userContext.UserId;
            _logger.LogDebug("Updating ToDo with id: {ToDoId} for userId: {UserId}", id, userId);

            var entity = await _repository.GetByIdAsync(id)
                ?? throw new ToDoNotFoundException(id);

            if (entity.UserId != userId)
                throw new ForbiddenException("You do not own this item");

            return (await _repository.UpdateAsync(entity.PatchFromContract(contract)))
                .ToContract();
        }

        public async Task DeleteAsync(Guid id)
        {
            var userId = _userContext.UserId;
            _logger.LogDebug("Deleting ToDo with id: {ToDoId} for userId: {UserId}", id, userId);

            var entity = await _repository.GetByIdAsync(id)
                ?? throw new ToDoNotFoundException(id);

            if (entity.UserId != userId)
                throw new ForbiddenException("You do not own this item");

            await _repository.DeleteAsync(entity);
        }
    }
}
