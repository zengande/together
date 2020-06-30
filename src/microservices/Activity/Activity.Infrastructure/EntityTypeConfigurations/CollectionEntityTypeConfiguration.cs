using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CollectionAggregate;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class CollectionEntityTypeConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable($"{ActivityDbContext.DbTablePrefix}Collections", ActivityDbContext.DbSchema);

            builder.Ignore(c => c.DomainEvents);
            builder.Ignore(c => c.Id);

            builder.Property(c => c.UserId)
                .HasMaxLength(100);
            builder.HasKey(c => new { c.UserId, c.ActivityId });

            builder.Property(c => c.CollectionTimeUtc)
                .IsRequired();

            builder.HasOne<Domain.AggregatesModel.ActivityAggregate.Activity>()
                .WithMany()
                .HasForeignKey(c => c.ActivityId)
                .IsRequired();
        }
    }
}
