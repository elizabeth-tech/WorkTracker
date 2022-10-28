using Microsoft.AspNetCore.Mvc;
using NLog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WorkTracker.API.Extensions;
using WorkTracker.Core.DTO.Input;
using WorkTracker.Data.Entities;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.API.Controllers
{
    [ApiController]
    [Route("api/report")]
    [Produces("application/json")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private static readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Получение списка отчетов для пользователя за указанный месяц
        /// </summary>
        [HttpGet("user/per-month")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IQueryable<Report>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetReportsOnUserPerMonth([Required] long userId, [Required] int month)
        {
            try
            {
                var reports = _reportService.GetReportsOnUserInMonth(userId, month);
                if (!reports.Any())
                    return NotFound("Для данного пользователя нет отчетов за указанный месяц");

                return Ok(reports);
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }


        /// <summary>
        /// Создание отчета для пользователя
        /// </summary>
        /// <param name="reportDTO"></param>
        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Report))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                var report = await _reportService.CreateReportAsync(reportDTO);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }

        /// <summary>
        /// Обновление данных отчета пользователя
        /// </summary>
        /// <param name="reportDTO"></param>
        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateReport([Required] long reportId, [FromBody] ReportDTO reportDTO)
        {
            try
            {
                await _reportService.UpdateReportAsync(reportId, reportDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="reportId"></param>
        [HttpDelete("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteReport([Required] long reportId)
        {
            try
            {
                await _reportService.DeleteReportAsync(reportId);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequestWithLog(ex, _logger);
            }
        }
    }
}
