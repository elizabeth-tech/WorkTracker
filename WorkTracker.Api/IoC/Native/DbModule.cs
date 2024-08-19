using Microsoft.EntityFrameworkCore;
using WorkTracker.Contracts.Models.Options;
using WorkTracker.DataAccess.Context;

namespace WorkTracker.Api.IoC.Native
{
    /// <summary>
    /// Модуль DI для базы данных
    /// </summary>
    public static class DbModule
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        /// <param name="services">Коллекция дескрипторов служб</param>
        /// <param name="configuration">Конфигурация приложения</param>
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = configuration.GetSection("DbOptions").Get<DbOptions>();

            var efCoreLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddConsole();
            });
            services.AddDbContext<WorkTrackerContext>(opts =>
                opts
                    .UseNpgsql(dbOptions.ConnectionString)
                    .UseLoggerFactory(efCoreLoggerFactory),
                contextLifetime: ServiceLifetime.Transient
            );
        }
    }
}
