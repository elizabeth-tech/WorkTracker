using Serilog;
using Serilog.Sinks.RollingFile.Extension;

namespace WorkTracker.Logging.Core.Logger
{
    /// <summary>
    /// Класс логирования информации и ошибок
    /// </summary>
    public class WorkTrackerLogger : IWorkTrackerLogger
    {
        protected ILogger errorLogger;

        private const int DaysLimit = 10;
        private const int FileSize = 10;

        public WorkTrackerLogger()
        {
            errorLogger = GetSerilog("error");
        }

        public void Error(
            string message,
            object src,
            Exception ex,
            string? className = null,
            string? methodName = null
        )
        {
            var msg = GetMessage(message, className, methodName);
            errorLogger.Error(ex, "{msg:}; {@src};", msg, src);
        }

        public void Error(
            string message,
            object? src = null,
            string? className = null,
            string? methodName = null
        )
        {
            var msg = GetMessage(message, className, methodName);
            if (src is null)
            {
                errorLogger.Error("{msg:l};", msg);
            }
            else
            {
                errorLogger.Error("{msg:l}; {@src}", msg, src);
            }
        }

        public void Error(
            string message,
            Exception ex,
            string? className = null,
            string? methodName = null
        )
        {
            var msg = GetMessage(message, className, methodName);
            errorLogger.Error(ex, "{msg:l};", msg);
        }

        public void Error(
            Exception ex,
            string? className = null,
            string? methodName = null
        )
        {
            var msg = GetMessage("", className, methodName);
            errorLogger.Error(ex, "{msg:l}", msg);
        }

        private static ILogger GetSerilog(string loggerName)
        {
            var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            var loggerConfiguration = new LoggerConfiguration()
                .WriteTo.SizeRollingFile(
                    pathFormat: Path.Combine(logFolder, loggerName + "-{Date}.log"), // имя файла
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, // уровень по умолчанию
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}", // формат по умолчанию
                    formatProvider: null, // тоже по умолчанию
                    fileSizeLimitBytes: 1024 * 1024 * FileSize,// размер файла лога ~ 10 мегабайт
                    retainedFileDurationLimit: TimeSpan.FromDays(DaysLimit)
                );

            return loggerConfiguration.CreateLogger();
        }

        private static string GetMessage(string mesage, string? className, string? methodName)
        {
            var str = string.Empty;

            if (!string.IsNullOrEmpty(className))
            {
                str = $"Class: {className};";
            }

            if (!string.IsNullOrEmpty(methodName))
            {
                str += $" Method: {methodName}; ";
            }

            return (str + mesage).Trim();
        }
    }
}
