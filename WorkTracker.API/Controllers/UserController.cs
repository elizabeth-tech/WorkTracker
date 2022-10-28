using Microsoft.AspNetCore.Mvc;
using NLog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WorkTracker.API.Extensions;
using WorkTracker.Core.DTO.Input;
using WorkTracker.Data.Entities;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private static readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        [HttpGet("user-list")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IQueryable<User>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                if (!users.Any())
                    return NotFound("На данный момент в БД нет пользователей");

                return Ok(users);
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = await _userService.CreateUserAsync(userDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser([Required] long userId, [FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateUserAsync(userId, userDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="userId"></param>
        [HttpDelete("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([Required] long userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }
    }
}
