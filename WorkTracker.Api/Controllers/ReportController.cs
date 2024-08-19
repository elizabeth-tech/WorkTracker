using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WorkTracker.BusinessLogic.Services.ReportService;
using WorkTracker.Contracts.Common;
using WorkTracker.Contracts.Models.Report;

namespace WorkTracker.Api.Controllers
{
    /// <summary>
	/// Контроллер для работы с отчетами
	/// </summary>
	[Route("report")]
    public class ReportController : Controller
    {
        /// <summary>
		/// Сервис для работы с отчетами
		/// </summary>
		private readonly IReportService _reportService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="reportService">Сервис для работы с отчетами</param>
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
		/// Получить список отчетов для пользователя за указанный месяц
		/// </summary>
		/// <param name="userId">ID пользователя</param>
        /// <param name="month">Номер месяца</param>
		/// <response code="200">Список отчетов успешно получен</response>
		/// <response code="400">Ошибка входящих параметров запроса или заданный пользователь не найден</response>
		/// <response code="500">Внутренняя ошибка сервера</response>
		/// <returns></returns>
		[HttpGet]
        [ProducesResponseType(typeof(ResultResponse<List<ReportOutDto>>), 200)]
        public async Task<ActionResult> GetReportsOnUserPerMonthAsync([Required] long userId, [Required] int month)
        {
            var response = await _reportService.GetReportsOnUserInMonthAsync(userId, month);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Создать отчет для пользователя
        /// </summary>
        /// <param name="reportInputDto">Входные данные отчета</param>
        /// <response code="200">Отчет успешно создан</response>
        /// <response code="400">Ошибка входящих параметров запроса</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResultResponse<ReportOutDto>), 200)]
        public async Task<ActionResult> CreateReportAsync([FromBody] ReportInputDto reportInputDto)
        {
            var response = await _reportService.CreateReportAsync(reportInputDto);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Обновить данные отчета пользователя
        /// </summary>
        /// <param name="reportId">ID отчета</param>
        /// <param name="reportInputDto">Входные данные отчета</param>
        /// <response code="200">Данные отчета успешно обновлены</response>
        /// <response code="400">Ошибка входящих параметров запроса или отчет не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ResultResponse<string>), 200)]
        public async Task<ActionResult> UpdateReportAsync([Required] long reportId, [FromBody] ReportInputDto reportInputDto)
        {
            var response = await _reportService.UpdateReportAsync(reportId, reportInputDto);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Удалить данные отчета пользователя
        /// </summary>
        /// <param name="reportId">ID отчета</param>
        /// <response code="200">Данные отчета успешно удалены</response>
        /// <response code="400">Ошибка входящих параметров запроса или отчет не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(ResultResponse<string>), 200)]
        public async Task<ActionResult> DeleteReportAsync([Required] long reportId)
        {
            var response = await _reportService.DeleteReportAsync(reportId);
            return response.StatusCode == StatusCodes.Status200OK
                ? Ok(response.Result)
                : StatusCode(response.StatusCode, response);
        }
    }
}
