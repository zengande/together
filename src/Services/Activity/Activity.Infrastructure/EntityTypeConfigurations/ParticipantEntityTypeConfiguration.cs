using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class ParticipantEntityTypeConfiguration
        : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participant");

            builder.HasKey(p => new { p.UserId, p.ActivityId });
            builder.Ignore(p => p.Id);
            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.Nickname)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.UserId)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(p => p.Avatar)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(p => p.JoinTime)
                .IsRequired();
            builder.Property(p => p.Sex)
                .HasDefaultValue(0);
            builder.Property(p => p.IsOwner)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
