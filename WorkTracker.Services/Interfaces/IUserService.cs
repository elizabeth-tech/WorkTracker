using WorkTracker.Core.DTO.Input;
using WorkTracker.Data.Entities;

namespace WorkTracker.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();

        Task<User> CreateUserAsync(UserDTO userDTO);

        Task UpdateUserAsync(long userId, UserDTO userDTO);

        Task DeleteUserAsync(long userId);
    }
}
