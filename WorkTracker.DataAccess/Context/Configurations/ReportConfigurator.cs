using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkTracker.DataAccess.Entities;

namespace WorkTracker.DataAccess.Context.Configurations
{
    internal class ReportConfigurator : BaseEntityTypeConfiguration<Report>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("report", "public");

            builder.Property(x => x.Id).HasColumnName("report_id").IsRequired();
            builder.HasKey(dto => dto.Id);

            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.Annotation).HasColumnName("annotation").IsRequired();
            builder.Property(x => x.Hours).HasColumnName("hours").IsRequired();
            builder.Property(x => x.Date).HasColumnName("date").IsRequired().HasColumnType("date");
        }
    }
}
