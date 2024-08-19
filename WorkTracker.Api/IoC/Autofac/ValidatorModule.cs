using Autofac;
using WorkTracker.BusinessLogic;

namespace WorkTracker.Api.IoC.Autofac
{
    /// <summary>
	/// Модуль DI для валидаторов
	/// </summary>
	public class ValidatorModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(BusinessLogicAssembly.Value)
                .Where(x => x.Name.EndsWith("Validator"))
                .InstancePerDependency();
        }
    }
}
