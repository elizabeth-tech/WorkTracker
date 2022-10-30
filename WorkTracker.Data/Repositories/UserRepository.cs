using WorkTracker.Data.Context;
using WorkTracker.Data.Entities;
using WorkTracker.Data.Interfaces;

namespace WorkTracker.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository<User>
    {
        public UserRepository(WorkTrackerContext context) : base(context) { }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        public IQueryable<User> GetAll() => _context.Users;

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (String.IsNullOrEmpty(user.Email))
                throw new Exception($"Email пользователя должен быть заполнен");
            if (String.IsNullOrEmpty(user.Surname))
                throw new Exception($"Фамилия пользователя должна быть заполнена");
            if (String.IsNullOrEmpty(user.Name))
                throw new Exception($"Имя пользователя должно быть заполнено");

            var emails = _context.Users.Where( x=> x.Email == user.Email);
            if(emails.Any())
                throw new Exception($"Такой email уже существует");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="user">Модель с новыми данными</param>
        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var dbUser = _context.Users.SingleOrDefault(t => t.Id == user.Id);
            if (dbUser == null)
                throw new Exception($"Пользователь с ID={user.Id} не найден в базе");

            if (String.IsNullOrEmpty(user.Email))
                throw new Exception($"Email пользователя должен быть заполнен");
            if (String.IsNullOrEmpty(user.Surname))
                throw new Exception($"Фамилия пользователя должна быть заполнена");
            if (String.IsNullOrEmpty(user.Name))
                throw new Exception($"Имя пользователя должно быть заполнено");

            var emails = _context.Users.Where(x => x.Email == user.Email && x.Id != user.Id);
            if (emails.Any())
                throw new Exception($"Такой email уже существует");

            dbUser.Email = user.Email;
            dbUser.Surname = user.Surname;
            dbUser.Name = user.Name;
            dbUser.Patronymic = user.Patronymic;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="userId"></param>
        public async Task DeleteUserAsync(long userId)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));

            var dbUser = _context.Users.SingleOrDefault(t => t.Id == userId);
            if (dbUser == null)
                throw new Exception($"Пользователь с ID={userId} не найден в базе");
     
            _context.Users.Remove(dbUser);
            var reports = _context.Reports.Where(x => x.UserId == userId);
            _context.Reports.RemoveRange(reports);
            await _context.SaveChangesAsync();
        }
    }
}
