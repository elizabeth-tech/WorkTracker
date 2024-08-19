using Autofac;
using WorkTracker.BusinessLogic;

namespace WorkTracker.Api.IoC.Autofac
{
    /// <summary>
    /// Модуль DI для сервисов
    /// </summary>
    public class ServiceModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(BusinessLogicAssembly.Value)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
