using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class AttendeeEntityTypeConfiguration
        : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.ToTable(ActivityDbContext.DbTablePrefix + "Attendees", ActivityDbContext.DbSchema);

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
