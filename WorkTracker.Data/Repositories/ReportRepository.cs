using WorkTracker.Data.Context;
using WorkTracker.Data.Entities;
using WorkTracker.Data.Interfaces;

namespace WorkTracker.Data.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository<Report>
    {
        public ReportRepository(WorkTrackerContext context) : base(context) { }

        /// <summary>
        /// Получение списка отчетов
        /// </summary>
        public IQueryable<Report> GetAll() => _context.Reports;

        /// <summary>
        /// Создание нового отчета
        /// </summary>
        public async Task<Report> CreateReportAsync(Report report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            if (report.UserId != 0)
            {
                var user = _context.Users.SingleOrDefault(t => t.Id == report.UserId);
                if (user == null)
                    throw new Exception($"Пользователя с ID={report.UserId} не существует. Невозможно создать отчет для такого пользователя");
            }
            else
                throw new Exception($"Невозможно создать отчет для пользователя с ID=0");

            if (String.IsNullOrEmpty(report.Annotation))
                throw new Exception($"Примечание отчета должно быть заполнено");
            if (report.Hours == 0)
                throw new Exception($"Нельзя указать 0 отработанных часов в отчете");
            if (report.Date == DateOnly.MinValue)
                throw new Exception($"Некорректная дата создания отчета");

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        /// <summary>
        /// Обновление данных по отчету
        /// </summary>
        /// <param name="report">Модель с новыми данными</param>
        public async Task UpdateReportAsync(Report report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            var dbReport = _context.Reports.SingleOrDefault(t => t.Id == report.Id);
            if (dbReport == null)
                throw new Exception($"Отчет с ID={report.Id} не найден в базе");

            if (report.UserId != 0)
            {
                var user = _context.Users.SingleOrDefault(t => t.Id == report.UserId);
                if (user == null)
                    throw new Exception($"Пользователя с ID={report.UserId} не существует. Невозможно создать отчет для такого пользователя");
                dbReport.UserId = report.UserId;
            }
            else
                throw new Exception($"Невозможно создать отчет для пользователя c ID=0");

            if (String.IsNullOrEmpty(report.Annotation))
                throw new Exception($"Примечание отчета должно быть заполнено");
            if (report.Hours == 0)
                throw new Exception($"Нельзя указать 0 отработанных часов в отчете");
            if (report.Date == DateOnly.MinValue)
                throw new Exception($"Некорректная дата создания отчета");
            
            dbReport.Annotation = report.Annotation;
            dbReport.Hours = report.Hours;          
            dbReport.Date = report.Date;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="reportId"></param>
        public async Task DeleteReportAsync(long reportId)
        {
            if (reportId == 0)
                throw new ArgumentNullException(nameof(reportId));

            var dbReport = _context.Reports.SingleOrDefault(t => t.Id == reportId);
            if (dbReport == null)
                throw new Exception($"Отчет с ID={reportId} не найден в базе");

            _context.Reports.Remove(dbReport);
            await _context.SaveChangesAsync();
        }
    }
}
