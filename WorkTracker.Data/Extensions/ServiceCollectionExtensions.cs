using Microsoft.Extensions.DependencyInjection;
using WorkTracker.Data.Entities;
using WorkTracker.Data.Interfaces;
using WorkTracker.Data.Repositories;

namespace WorkTracker.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IReportRepository<Report>, ReportRepository>();
            return services;
        }
    }
}
