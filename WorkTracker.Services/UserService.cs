using AutoMapper;
using NLog;
using WorkTracker.Core.DTO.Input;
using WorkTracker.Data.Entities;
using WorkTracker.Data.Interfaces;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository<User> _userRepository;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public UserService(IUserRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
       

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        public async Task<User> CreateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(userDTO));

            var user = _mapper.Map<User>(userDTO);

            var newUser = await _userRepository.CreateUserAsync(user);
            return newUser;
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userDTO">Модель с новыми данными</param>
        public async Task UpdateUserAsync(long userId, UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(userDTO));

            var user = _mapper.Map<User>(userDTO);
            user.Id = userId;
            await _userRepository.UpdateUserAsync(user);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="userId"></param>
        public async Task DeleteUserAsync(long userId)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));

            await _userRepository.DeleteUserAsync(userId);
        }
    }
}
