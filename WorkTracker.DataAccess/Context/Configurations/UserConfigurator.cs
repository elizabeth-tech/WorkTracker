using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkTracker.DataAccess.Entities;

namespace WorkTracker.DataAccess.Context.Configurations
{
    internal class UserConfigurator : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "public");

            builder.Property(x => x.Id).HasColumnName("user_id").IsRequired();
            builder.HasKey(dto => dto.Id);

            builder.Property(x => x.Email).HasColumnName("email").IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Surname).HasColumnName("surname").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").IsRequired();
            builder.Property(x => x.Patronymic).HasColumnName("patronymic");
        }
    }
}
