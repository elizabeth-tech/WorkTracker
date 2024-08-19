namespace WorkTracker.Api.IoC.Native
{
    /// <summary>
	/// Основной модуль DI
	/// </summary>
	public class NativeAppModule
    {
        /// <inheritdoc />
        public NativeAppModule(IServiceCollection services, IConfiguration configuration)
        {
            OptionsModule.Load(services, configuration);
            DbModule.Load(services, configuration);
            SwaggerModule.Load(services);
        }
    }
}
