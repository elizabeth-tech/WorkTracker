using WorkTracker.Contracts.Common;
using WorkTracker.Contracts.Models.Report;

namespace WorkTracker.BusinessLogic.Services.ReportService
{
    /// <summary>
    /// Интерфейс сервиса отчетов
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получение отчетов пользователя за указанный месяц
        /// </summary>
        /// <param name="userId">ID пользователя, для которого получаем отчеты</param>
        /// <param name="month">Номер месяца</param>
        Task<ResultResponse<List<ReportOutDto>>> GetReportsOnUserInMonthAsync(long userId, int month);

        /// <summary>
        /// Создание нового отчета
        /// </summary>
        /// <param name="reportInputDto">Новый отчет</param>
        Task<ResultResponse<ReportOutDto>> CreateReportAsync(ReportInputDto reportInputDto);

        /// <summary>
        /// Обновление данных по отчету
        /// </summary>
        /// <param name="reportId">ID изменяемого отчета</param>
        /// <param name="reportInputDto">Новые данные отчета</param>
        Task<ResultResponse<string>> UpdateReportAsync(long reportId, ReportInputDto reportInputDto);

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="reportId">ID удаляемого отчета</param>
        Task<ResultResponse<string>> DeleteReportAsync(long reportId);
    }
}
