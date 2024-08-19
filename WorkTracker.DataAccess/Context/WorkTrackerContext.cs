using Microsoft.EntityFrameworkCore;
using WorkTracker.DataAccess.Context.Configurations;
using WorkTracker.DataAccess.Entities;

namespace WorkTracker.DataAccess.Context
{
    /// <summary>
	/// Контекст БД
	/// </summary>
	public class WorkTrackerContext : DbContext
    {
        public WorkTrackerContext(DbContextOptions<WorkTrackerContext> options)
            : base(options) { }

        public DbSet<Report> Reports { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfigurator());
            builder.ApplyConfiguration(new ReportConfigurator());
        }
    }
}
