using WorkTracker.Data.Entities;

namespace WorkTracker.Data.Interfaces
{
    public interface IUserRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<User> CreateUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(long userId);
    }
}
