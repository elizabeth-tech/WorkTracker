using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WorkTracker.DataAccess.Context;
using WorkTracker.DataAccess.Entities;

namespace WorkTracker.BusinessLogic.Infrastructure
{
    public static class SeedData
    {
        public static async void Generate(IApplicationBuilder app)
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-2);

            List<User> users = new()
            {
                new User { Email = "kristina@gmail.com", Name = "Кристина", Surname = "Иванова", Patronymic = "Валерьевна" },
                new User { Email = "evgen@gmail.com", Name = "Евгений", Surname = "Сидоров", Patronymic = "Петрович" },
            };

            List<Report> reports = new()
            {
                new Report { UserId = 1, Annotation = "Тест запись", Hours = 8, Date = date },
                new Report { UserId = 1, Annotation = "Тест запись", Hours = 8, Date = date },
                new Report { UserId = 2, Annotation = "Тест запись", Hours = 8, Date = date },
            };

            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WorkTrackerContext>();

            if (!context.Users.Any() && !context.Reports.Any())
            {
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
                await context.Reports.AddRangeAsync(reports);
                await context.SaveChangesAsync();
            }
        }
    }
}
