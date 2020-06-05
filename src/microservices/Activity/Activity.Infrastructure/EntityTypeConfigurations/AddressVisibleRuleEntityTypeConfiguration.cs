using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class AddressVisibleRuleEntityTypeConfiguration
        : IEntityTypeConfiguration<AddressVisibleRule>
    {
        public void Configure(EntityTypeBuilder<AddressVisibleRule> builder)
        {
            builder.ToTable(ActivityDbContext.DbTablePrefix + "AddressVisibleRules", ActivityDbContext.DbSchema);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasData(AddressVisibleRule.List());
        }
    }
}
