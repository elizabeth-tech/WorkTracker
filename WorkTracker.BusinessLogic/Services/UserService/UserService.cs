using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkTracker.BusinessLogic.Validators.Report;
using WorkTracker.BusinessLogic.Validators.User;
using WorkTracker.Contracts.Common;
using WorkTracker.Contracts.Models.Report;
using WorkTracker.Contracts.Models.User;
using WorkTracker.DataAccess.Context;
using WorkTracker.DataAccess.Entities;
using WorkTracker.Logging.Core.Logger;

namespace WorkTracker.BusinessLogic.Services.UserService
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly IWorkTrackerLogger _logger;

        /// <summary>
		/// Контекст БД
		/// </summary>
		private readonly WorkTrackerContext _workTrackerContext;

        /// <summary>
        /// Валидатор модели пользователя
        /// </summary>
        private UserInputDtoValidator _userInputDtoValidator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="logger">Логгер</param>
        /// <param name="workTrackerContext">Контекст БД</param>
        /// <param name="userInputDtoValidator">Валидатор модели пользователя</param>
        public UserService(
            IMapper mapper,
            IWorkTrackerLogger logger,
            WorkTrackerContext workTrackerContext,
            UserInputDtoValidator userInputDtoValidator
        )
        {
            _mapper = mapper;
            _logger = logger;
            _workTrackerContext = workTrackerContext;
            _userInputDtoValidator = userInputDtoValidator;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        public async Task<ResultResponse<List<UserOutDto>>> GetUsersAsync()
        {
            try
            {
                var users = await _workTrackerContext.Users.ToListAsync();
                var userOutDto = _mapper.Map<List<User>, List<UserOutDto>>(users);

                return ResultResponse<List<UserOutDto>>.GetSuccessResponse(userOutDto);
            }
            catch (Exception ex)
            {
                string error = "Ошибка при получении списка пользователей";
                _logger.Error(
                     message: error,
                     ex: ex,
                     className: GetType().Name,
                     methodName: nameof(GetUsersAsync)
                 );
                return ResultResponse<List<UserOutDto>>.GetInternalErrorResponse(error);
            }
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="userInputDto">Входные данные пользователя</param>
        public async Task<ResultResponse<UserOutDto>> CreateUserAsync(UserInputDto userInputDto)
        {
            try
            {
                var validationDtoResult = await _userInputDtoValidator.ValidateAsync(userInputDto);
                if (!validationDtoResult.IsValid)
                {
                    var firstError = validationDtoResult.Errors.First();
                    return ResultResponse<UserOutDto>.GetBadRequestResponse(firstError.ErrorMessage);
                }

                var user = _mapper.Map<User>(userInputDto);
                var newUser = await _workTrackerContext.Users.AddAsync(user);
                await _workTrackerContext.SaveChangesAsync();
                var userOutDto = _mapper.Map<UserOutDto>(newUser.Entity);

                return ResultResponse<UserOutDto>.GetSuccessResponse(userOutDto);
            }
            catch (Exception ex)
            {
                string error = "Ошибка при создании нового пользователя";
                _logger.Error(
                    message: error,
                    ex: ex,
                    className: GetType().Name,
                    methodName: nameof(CreateUserAsync)
                );
                return ResultResponse<UserOutDto>.GetBadRequestResponse(error);
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="userInputDto">Новые данные пользователя</param>
        public async Task<ResultResponse<string>> UpdateUserAsync(long userId, UserInputDto userInputDto)
        {
            try
            {
                var validationDtoResult = await _userInputDtoValidator.ValidateAsync(userInputDto);
                if (!validationDtoResult.IsValid)
                {
                    var firstError = validationDtoResult.Errors.First();
                    return ResultResponse<string>.GetBadRequestResponse(firstError.ErrorMessage);
                }

                var dbUser = await _workTrackerContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (dbUser is null)
                {
                    string error = $"Пользователь с ID={userId} не найден в базе";
                    _logger.Error(
                       message: error,
                       src: userId,
                       className: GetType().Name,
                       methodName: nameof(UpdateUserAsync)
                    );
                    return ResultResponse<string>.GetBadRequestResponse(error);
                }

                dbUser.Email = userInputDto.Email;
                dbUser.Surname = userInputDto.Surname;
                dbUser.Name = userInputDto.Name;
                dbUser.Patronymic = userInputDto.Patronymic;
                await _workTrackerContext.SaveChangesAsync();

                return ResultResponse<string>.GetSuccessResponse($"Данные пользователя с ID={userId} успешно обновлены");
            }
            catch (Exception ex)
            {
                string error = $"Ошибка при обновлении пользователя c ID={userId}";
                _logger.Error(
                    message: error,
                    ex: ex,
                    src: userInputDto,
                    className: GetType().Name,
                    methodName: nameof(UpdateUserAsync)
                );
                return ResultResponse<string>.GetBadRequestResponse(error);
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        public async Task<ResultResponse<string>> DeleteUserAsync(long userId)
        {
            try
            {
                var dbUser = await _workTrackerContext.Users.FirstOrDefaultAsync(t => t.Id == userId);
                if (dbUser == null)
                {
                    string error = $"Пользователь с ID={userId} не найден в базе";
                    _logger.Error(
                       message: error,
                       src: userId,
                       className: GetType().Name,
                       methodName: nameof(DeleteUserAsync)
                    );
                    return ResultResponse<string>.GetBadRequestResponse(error);
                }

                _workTrackerContext.Users.Remove(dbUser);
                var reports = _workTrackerContext.Reports.Where(x => x.UserId == userId);
                _workTrackerContext.Reports.RemoveRange(reports);
                await _workTrackerContext.SaveChangesAsync();

                return ResultResponse<string>.GetSuccessResponse($"Пользователь с ID={userId} успешно удален");
            }
            catch (Exception ex)
            {
                string error = $"Ошибка при удалении пользователя c ID={userId}";
                _logger.Error(
                    message: error,
                    ex: ex,
                    src: userId,
                    className: GetType().Name,
                    methodName: nameof(DeleteUserAsync)
                );
                return ResultResponse<string>.GetBadRequestResponse(error);
            }
        }
    }
}
