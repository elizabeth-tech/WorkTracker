using WorkTracker.Services;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
