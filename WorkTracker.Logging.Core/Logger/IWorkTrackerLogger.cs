namespace WorkTracker.Logging.Core.Logger
{
    /// <summary>
	/// Интерфейс для логирования информации и ошибок
	/// </summary>
    public interface IWorkTrackerLogger
    {
        /// <summary>
		/// Запись ошибки в лог
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="src">Объект для логирования</param>
		/// <param name="ex">Ошибка</param>
		/// <param name="className">Название класса, откуда вызвана запись</param>
		/// <param name="methodName">Название метода, откуда вызвана запись</param>
		void Error(string message, object src, Exception ex, string? className = null, string? methodName = null);

        /// <summary>
        /// Запись ошибки в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="src">Объект для логирования</param>
        /// <param name="className">Название класса, откуда вызвана запись</param>
        /// <param name="methodName">Название метода, откуда вызвана запись</param>
        void Error(string message, object? src = null, string? className = null, string? methodName = null);

        /// <summary>
        /// Запись ошибки в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ex">Ошибка</param>
        /// <param name="className">Название класса, откуда вызвана запись</param>
        /// <param name="methodName">Название метода, откуда вызвана запись</param>
        void Error(string message, Exception ex, string? className = null, string? methodName = null);

        /// <summary>
        /// Запись ошибки в лог
        /// </summary>
        /// <param name="ex">Ошибка</param>
        /// <param name="className">Название класса, откуда вызвана запись</param>
        /// <param name="methodName">Название метода, откуда вызвана запись</param>
        void Error(Exception ex, string? className = null, string? methodName = null);
    }
}
