using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WorkTracker.BusinessLogic.Services.UserService;
using WorkTracker.Contracts.Common;
using WorkTracker.Contracts.Models.User;

namespace WorkTracker.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [Route("user")]
    public class UserController : Controller
    {
        /// <summary>
        /// Сервис для работы с пользователями
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <response code="200">Список пользователей успешно получен</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResultResponse<List<UserOutDto>>), 200)]
        public async Task<ActionResult> GetUsersAsync()
        {
            var response = await _userService.GetUsersAsync();
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="userInputDto">Входные данные пользователя</param>
        /// <response code="200">Пользователь успешно создан</response>
        /// <response code="400">Ошибка входящих параметров запроса</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResultResponse<UserOutDto>), 200)]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserInputDto userInputDto)
        {
            var response = await _userService.CreateUserAsync(userInputDto);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="userInputDto">Входные данные пользователя</param>
        /// <response code="200">Данные пользователя успешно обновлены</response>
        /// <response code="400">Ошибка входящих параметров запроса</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ResultResponse<string>), 200)]
        public async Task<ActionResult> UpdateUserAsync([Required] long userId, [FromBody] UserInputDto userInputDto)
        {
            var response = await _userService.UpdateUserAsync(userId, userInputDto);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <response code="200">Пользователь успешно удален</response>
        /// <response code="400">Ошибка входящих параметров запроса</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(ResultResponse<string>), 200)]
        public async Task<ActionResult> DeleteUserAsync([Required] long userId)
        {
            var response = await _userService.DeleteUserAsync(userId);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }
    }
}
