using WorkTracker.Services.Infrastructure;

namespace WorkTracker.API.Extensions
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            return services;
        }
    }
}
