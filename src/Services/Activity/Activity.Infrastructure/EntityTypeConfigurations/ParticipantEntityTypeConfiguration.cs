using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class ParticipantEntityTypeConfiguration
        : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participant");

            builder.HasKey(o => o.Id);

            builder.Ignore(p => p.DomainEvents);

            builder.Property(o => o.Id)
               .ForSqlServerUseSequenceHiLo("participantseq");
            builder.Property(p => p.Nickname)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.Avatar)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(p => p.JoinTime)
                .IsRequired();
            builder.Property(p => p.Sex)
                .HasDefaultValue(0);
        }
    }
}
