using WorkTracker.Data.Entities;

namespace WorkTracker.Data.Interfaces
{
    public interface IReportRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<Report> CreateReportAsync(Report report);

        Task UpdateReportAsync(Report report);

        Task DeleteReportAsync(long reportId);
    }
}
