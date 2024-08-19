namespace WorkTracker.Contracts.Models.User
{
    /// <summary>
    /// Выходная модель данных пользователя
    /// </summary>
    public class UserOutDto
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }
    }
}
