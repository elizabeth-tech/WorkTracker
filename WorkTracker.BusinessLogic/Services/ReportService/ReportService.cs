using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WorkTracker.BusinessLogic.Validators.Report;
using WorkTracker.Contracts.Common;
using WorkTracker.Contracts.Models.Report;
using WorkTracker.DataAccess.Context;
using WorkTracker.DataAccess.Entities;
using WorkTracker.Logging.Core.Logger;

namespace WorkTracker.BusinessLogic.Services.ReportService
{
    /// <summary>
    /// Сервис для работы с отчетами
    /// </summary>
    public class ReportService : IReportService
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly IWorkTrackerLogger _logger;

        /// <summary>
		/// Контекст БД
		/// </summary>
		private readonly WorkTrackerContext _workTrackerContext;

        /// <summary>
		/// Валидатор модели отчета
		/// </summary>
		private ReportInputDtoValidator _reportInputDtoValidator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="logger">Логгер</param>
        /// <param name="workTrackerContext">Контекст БД</param>
        /// <param name="reportInputDtoValidator">Валидатор модели отчета</param>
        public ReportService(
            IMapper mapper,
            IWorkTrackerLogger logger,
            WorkTrackerContext workTrackerContext,
            ReportInputDtoValidator reportInputDtoValidator
        )
        {
            _mapper = mapper;
            _logger = logger;
            _workTrackerContext = workTrackerContext;
            _reportInputDtoValidator = reportInputDtoValidator;
        }

        /// <summary>
        /// Получение отчетов пользователя за указанный месяц
        /// </summary>
        /// <param name="userId">ID пользователя, для которого получаем отчеты</param>
        /// <param name="month">Номер месяца</param>
        public async Task<ResultResponse<List<ReportOutDto>>> GetReportsOnUserInMonthAsync(long userId, int month)
        {
            try
            {
                var user = await _workTrackerContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (user is null)
                {
                    string error = $"Невозможно получить отчеты. Пользователя с ID={userId} не существует";
                    _logger.Error(
                        message: error,
                        src: userId,
                        className: GetType().Name,
                        methodName: nameof(GetReportsOnUserInMonthAsync)
                    );
                    return ResultResponse<List<ReportOutDto>>.GetBadRequestResponse(error);
                }

                var reportsData = await _workTrackerContext.Reports
                    .Where(x => x.Date.Month == month && x.UserId == userId)
                    .ToListAsync();

                var reports = _mapper.Map<List<ReportOutDto>>(reportsData);
                return ResultResponse<List<ReportOutDto>>.GetSuccessResponse(reports);
            }
            catch (Exception ex)
            {
                string error = "Ошибка при получении отчетов пользователя за указанный месяц";
                _logger.Error(
                    message: error,
                    ex: ex,
                    src: userId,
                    className: GetType().Name,
                    methodName: nameof(GetReportsOnUserInMonthAsync)
                );
                return ResultResponse<List<ReportOutDto>>.GetBadRequestResponse(error);
            }
        }

        /// <summary>
        /// Создание нового отчета
        /// </summary>
        /// <param name="reportInputDto">Новый отчет</param>
        public async Task<ResultResponse<ReportOutDto>> CreateReportAsync(ReportInputDto reportInputDto)
        {
            try
            {
                var validationDtoResult = await _reportInputDtoValidator.ValidateAsync(reportInputDto);
                if (!validationDtoResult.IsValid)
                {
                    var firstError = validationDtoResult.Errors.First();
                    return ResultResponse<ReportOutDto>.GetBadRequestResponse(firstError.ErrorMessage);
                }

                var user = await _workTrackerContext.Users.FirstOrDefaultAsync(x => x.Id == reportInputDto.UserId);
                if (user is null)
                {
                    string error = "Такой ID пользователя не существует";
                    _logger.Error(
                         message: error,
                         src: reportInputDto,
                         className: GetType().Name,
                         methodName: nameof(CreateReportAsync)
                     );
                    return ResultResponse<ReportOutDto>.GetBadRequestResponse(error);
                }

                var reportData = _mapper.Map<Report>(reportInputDto);
                var newReport = await _workTrackerContext.Reports.AddAsync(reportData);
                await _workTrackerContext.SaveChangesAsync();
                var outReportData = _mapper.Map<ReportOutDto>(newReport.Entity);

                return ResultResponse<ReportOutDto>.GetSuccessResponse(outReportData);
            }
            catch (Exception ex)
            {
                string error = "Ошибка при создании нового отчета";
                _logger.Error(
                     message: error,
                     ex: ex,
                     src: reportInputDto,
                     className: GetType().Name,
                     methodName: nameof(CreateReportAsync)
                 );
                return ResultResponse<ReportOutDto>.GetBadRequestResponse(error);
            }
        }

        /// <summary>
        /// Обновление данных по отчету
        /// </summary>
        /// <param name="reportId">ID изменяемого отчета</param>
        /// <param name="reportInputDto">Новые данные отчета</param>
        public async Task<ResultResponse<string>> UpdateReportAsync(long reportId, ReportInputDto reportInputDto)
        {
            try
            {
                var validationDtoResult = await _reportInputDtoValidator.ValidateAsync(reportInputDto);
                if (!validationDtoResult.IsValid)
                {
                    var firstError = validationDtoResult.Errors.First();
                    return ResultResponse<string>.GetBadRequestResponse(firstError.ErrorMessage);
                }

                var dbReport = await _workTrackerContext.Reports.FirstOrDefaultAsync(x => x.Id == reportId);
                if (dbReport is null)
                {
                    string error = $"Отчет c ID={reportId} не найден";
                    _logger.Error(
                       message: error,
                       src: reportId,
                       className: GetType().Name,
                       methodName: nameof(UpdateReportAsync)
                    );
                    return ResultResponse<string>.GetBadRequestResponse(error);
                }

                dbReport.Annotation = reportInputDto.Annotation;
                dbReport.Hours = reportInputDto.Hours;
                dbReport.Date = DateOnly.FromDateTime(reportInputDto.Date);
                await _workTrackerContext.SaveChangesAsync();

                return ResultResponse<string>.GetSuccessResponse($"Отчет c ID={reportId} успешно обновлен");
            }
            catch (Exception ex)
            {
                string error = $"Ошибка при обновлении отчета c ID={reportId} ";
                _logger.Error(
                    message: error,
                    ex: ex,
                    src: reportInputDto,
                    className: GetType().Name,
                    methodName: nameof(UpdateReportAsync)
                );
                return ResultResponse<string>.GetBadRequestResponse(error);
            }
        }

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="reportId"></param>
        public async Task<ResultResponse<string>> DeleteReportAsync(long reportId)
        {
            try
            {
                var dbReport = await _workTrackerContext.Reports.FirstOrDefaultAsync(t => t.Id == reportId);
                if (dbReport is null)
                {
                    string error = $"Отчет c ID={reportId} не найден";
                    _logger.Error(
                      message: error,
                      src: reportId,
                      className: GetType().Name,
                      methodName: nameof(DeleteReportAsync)
                    );
                    return ResultResponse<string>.GetBadRequestResponse(error);
                }

                _workTrackerContext.Reports.Remove(dbReport);
                await _workTrackerContext.SaveChangesAsync();

                return ResultResponse<string>.GetSuccessResponse($"Отчет c ID={reportId} успешно удален");
            }
            catch (Exception ex)
            {
                string error = $"Ошибка при удалении отчета c ID={reportId}";
                _logger.Error(
                   message: error,
                   ex: ex,
                   src: reportId,
                   className: GetType().Name,
                   methodName: nameof(DeleteReportAsync)
                );
                return ResultResponse<string>.GetBadRequestResponse(error);
            }
        }
    }
}
