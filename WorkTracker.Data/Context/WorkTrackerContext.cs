using Microsoft.EntityFrameworkCore;
using WorkTracker.Data.Context.Configurations;
using WorkTracker.Data.Entities;

namespace WorkTracker.Data.Context
{
    public class WorkTrackerContext : DbContext
    {
        public WorkTrackerContext(DbContextOptions<WorkTrackerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Report> Reports { get; set; }

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
