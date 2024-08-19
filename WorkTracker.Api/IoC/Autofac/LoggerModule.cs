using Autofac;
using WorkTracker.Logging.Core.Logger;

namespace WorkTracker.Api.IoC.Autofac
{
    /// <summary>
	/// Модуль DI для логирования
	/// </summary>
	public class LoggerModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
            .Register(_ => new WorkTrackerLogger())
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
