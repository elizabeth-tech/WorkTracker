using Autofac;

namespace WorkTracker.Api.IoC.Autofac
{
    /// <summary>
	/// Основной модуль DI
	/// </summary>
    public class AppModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<LoggerModule>();
            builder.RegisterModule<AutoMapperModule>();
            builder.RegisterModule<ControllerModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ValidatorModule>();
        }
    }
}
