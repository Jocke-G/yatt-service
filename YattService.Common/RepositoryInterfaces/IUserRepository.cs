namespace YattService.Common.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task EnsureUserExistsAsync(string userId);
        Task UpdateLastLoginIfOldAsync(string userId);
    }
}
