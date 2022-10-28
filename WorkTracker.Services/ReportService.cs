using AutoMapper;
using NLog;
using WorkTracker.Core.DTO.Input;
using WorkTracker.Core.DTO.Output;
using WorkTracker.Data.Entities;
using WorkTracker.Data.Interfaces;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository<Report> _reportRepository;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ReportService(IReportRepository<Report> reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание нового отчета
        /// </summary>
        /// <param name="reportDTO">Модель с новыми данными</param>
        public async Task<Core.DTO.Output.ReportDTO> CreateReportAsync(Core.DTO.Input.ReportDTO reportDTO)
        {
            if (reportDTO == null)
                throw new ArgumentNullException(nameof(reportDTO));

            var reportData = _mapper.Map<Report>(reportDTO);

            var newReport = await _reportRepository.CreateReportAsync(reportData);
            var report = _mapper.Map<Core.DTO.Output.ReportDTO>(newReport);
            return report;
        }

        /// <summary>
        /// Обновление данных по отчету
        /// </summary>
        /// <param name="reportDTO">Модель с новыми данными</param>
        public async Task UpdateReportAsync(long reportId, Core.DTO.Input.ReportDTO reportDTO)
        {
            if (reportDTO == null)
                throw new ArgumentNullException(nameof(reportDTO));

            var report = _mapper.Map<Report>(reportDTO);
            report.Id = reportId;

            await _reportRepository.UpdateReportAsync(report);
        }

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="reportId"></param>
        public async Task DeleteReportAsync(long reportId)
        {
            if (reportId == 0)
                throw new ArgumentNullException(nameof(reportId));

            await _reportRepository.DeleteReportAsync(reportId);
        }

        /// <summary>
        /// Получение отчетов пользователя за указанный месяц
        /// </summary>
        /// <param name="userId">ID пользователя, для которого получаем отчеты</param>
        /// <param name="month">Номер месяца</param>
        public IEnumerable<Core.DTO.Output.ReportDTO> GetReportsOnUserInMonth(long userId, int month)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));

            if (month == 0)
                throw new ArgumentNullException(nameof(month));

            var reportsData = _reportRepository.GetAll().Where(x => x.Date.Month == month && x.UserId == userId).ToList();
            var reports = _mapper.Map<IEnumerable<Core.DTO.Output.ReportDTO>>(reportsData);
            return reports;
        }
    }
}
