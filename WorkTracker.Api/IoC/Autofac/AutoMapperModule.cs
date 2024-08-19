using Autofac;
using AutoMapper;
using WorkTracker.BusinessLogic.MapperProfiles;

namespace WorkTracker.Api.IoC.Autofac
{
    /// <summary>
	/// Модуль DI для AutoMapper
	/// </summary>
	public class AutoMapperModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => new MapperConfiguration(
                    cfg =>
                    {
                        cfg.AddProfile<UserProfile>();
                        cfg.AddProfile<ReportProfile>();
                    }
                ).CreateMapper())
                .As<IMapper>()
                .SingleInstance();
        }
    }
}
