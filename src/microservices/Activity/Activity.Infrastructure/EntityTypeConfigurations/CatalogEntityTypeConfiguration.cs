using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class CatalogEntityTypeConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.ToTable(ActivityDbContext.DbTablePrefix + "Catalogs", ActivityDbContext.DbSchema);

            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.DomainEvents);

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();

            builder.Property(c => c.Order)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property<int?>("_parentId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ParentId");

            var navigation = builder.Metadata
                .FindNavigation(nameof(Catalog.Children));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<Catalog>()
                .WithMany(c => c.Children)
                .HasForeignKey("ParentId");
        }
    }
}
