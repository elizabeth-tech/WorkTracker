using WorkTracker.Contracts.Common;
using WorkTracker.Contracts.Models.User;

namespace WorkTracker.BusinessLogic.Services.UserService
{
    /// <summary>
    /// Интерфейс сервиса пользователей
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        Task<ResultResponse<List<UserOutDto>>> GetUsersAsync();

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="userInputDto">Входные данные пользователя</param>
        Task<ResultResponse<UserOutDto>> CreateUserAsync(UserInputDto userInputDto);

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="userInputDto">Новые данные пользователя</param>
        Task<ResultResponse<string>> UpdateUserAsync(long userId, UserInputDto userInputDto);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        Task<ResultResponse<string>> DeleteUserAsync(long userId);
    }
}
