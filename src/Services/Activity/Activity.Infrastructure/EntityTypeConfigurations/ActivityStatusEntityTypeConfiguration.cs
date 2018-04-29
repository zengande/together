using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class ActivityStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<ActivityStatus>
    {
        public void Configure(EntityTypeBuilder<ActivityStatus> builder)
        {
            builder.ToTable("activitystatus");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
