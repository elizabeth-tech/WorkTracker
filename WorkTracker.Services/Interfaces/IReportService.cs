using WorkTracker.Core.DTO.Input;

namespace WorkTracker.Services.Interfaces
{
    public interface IReportService
    {
        Task<Core.DTO.Output.ReportDTO> CreateReportAsync(ReportDTO reportDTO);

        Task UpdateReportAsync(long reportId, ReportDTO reportDTO);

        Task DeleteReportAsync(long reportId);

        IEnumerable<Core.DTO.Output.ReportDTO> GetReportsOnUserInMonth(long userId, int month);
    }
}
