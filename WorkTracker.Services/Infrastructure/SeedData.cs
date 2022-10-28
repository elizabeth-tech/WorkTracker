using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WorkTracker.Data.Context;
using WorkTracker.Data.Entities;

namespace WorkTracker.Services.Infrastructure
{
    public static class SeedData
    {
        public static void Generate(IApplicationBuilder app)
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-2);

            List<User> users = new List<User>()
            {
                new User { Id = 1, Email = "kristina@gmail.com", Name = "Кристина", Surname = "Иванова", Patronymic = "Валерьевна" },
                new User { Id = 2, Email = "evgen@gmail.com", Name = "Евгений", Surname = "Сидоров", Patronymic = "Петрович" },
            };

            List<Report> reports = new List<Report>()
            {
                new Report { Id = 1, UserId = 1, Annotation = "Тест запись", Hours = 8, Date = date },
                new Report { Id = 2, UserId = 1, Annotation = "Тест запись", Hours = 8, Date = date },
                new Report { Id = 3, UserId = 2, Annotation = "Тест запись", Hours = 8, Date = date },
            };

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WorkTrackerContext>();

                foreach (var item in users)
                {
                    var existed = context.Users.Find(item.Id);
                    if (existed != null)
                        context.Entry(existed).CurrentValues.SetValues(item);
                    else
                        context.Users.Add(item);
                }

                foreach (var item in reports)
                {
                    var existed = context.Reports.Find(item.Id);
                    if (existed != null)
                        context.Entry(existed).CurrentValues.SetValues(item);
                    else
                        context.Reports.Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}
