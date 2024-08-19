using Autofac;
using Autofac.Features.AttributeFilters;
using System.Reflection;
using Module = Autofac.Module;

namespace WorkTracker.Api.IoC.Autofac
{
    /// <summary>
    /// Модуль DI для контроллеров
    /// </summary>
    public class ControllerModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.Name.EndsWith("Controller"))
                .WithAttributeFiltering()
                .PropertiesAutowired();
        }
    }
}
