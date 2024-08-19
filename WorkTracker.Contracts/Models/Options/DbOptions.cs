using Npgsql;

namespace WorkTracker.Contracts.Models.Options
{
    /// <summary>
	/// Настройки БД
	/// </summary>
	public class DbOptions
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Имя БД
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Пул соединений
        /// </summary>
        public bool Pooling { get; set; }

        /// <summary>
        /// Схемы
        /// </summary>
        public string SearchPath { get; set; }

        /// <summary>
        /// Таймаут между началом выполнения команды и генерации исключения
        /// </summary>
        public int CommandTimeout { get; set; }

        /// <summary>
        /// Имя проекта
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Получить строку подключения к БД
        /// </summary>
        /// <returns></returns>
        public string ConnectionString =>
            new NpgsqlConnectionStringBuilder
            {
                Host = Host,
                Port = Port,
                Database = Database,
                Username = User,
                Password = Password,
                SearchPath = string.IsNullOrEmpty(SearchPath) ? null : SearchPath,
                Pooling = Pooling,
                CommandTimeout = CommandTimeout,
                ApplicationName = ApplicationName
            }.ConnectionString;
    }
}
