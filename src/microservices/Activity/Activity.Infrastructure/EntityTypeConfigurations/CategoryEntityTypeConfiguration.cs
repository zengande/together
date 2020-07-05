using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Infrastructure.EntityFrameworkCore;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(ActivityDbContext.DbTablePrefix + "Categories", ActivityDbContext.DbSchema);

            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.DomainEvents);

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();

            builder.Property(c => c.Image);

            builder.Property(c => c.Order)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
