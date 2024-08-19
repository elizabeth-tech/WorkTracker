using WorkTracker.Contracts.Models.Options;

namespace WorkTracker.Api.IoC.Native
{
    /// <summary>
    /// Модуль DI для общих настроек
    /// </summary>
    public static class OptionsModule
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        /// <param name="services">Коллекция дескрипторов служб</param>
        /// <param name="configuration">Конфигурация приложения</param>
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DbOptions>().Bind(configuration.GetSection(nameof(DbOptions)));
        }
    }
}
