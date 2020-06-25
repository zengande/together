using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class ActivityEntityTypeConfiguration
        : IEntityTypeConfiguration<Domain.AggregatesModel.ActivityAggregate.Activity>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.ActivityAggregate.Activity> builder)
        {
            builder.ToTable(ActivityDbContext.DbTablePrefix + "Activities", ActivityDbContext.DbSchema);

            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.DomainEvents);

            builder.Property(a => a.ActivityStartTime)
                .IsRequired();

            builder.Property(a => a.ActivityEndTime)
                .IsRequired();

            builder.Property(a => a.CreateTime)
                .IsRequired();

            builder.Property(a => a.CreatorId)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(a => a.Title)
                .IsRequired(false);

            builder.Property(a => a.Content)
                .IsRequired(false);

            builder.Property(a => a.EndRegisterTime)
                .IsRequired();

            builder.Property(a => a.LimitsNum);

            var addressBuilder = builder.OwnsOne(a => a.Address);
            addressBuilder.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();
            addressBuilder.Property(a => a.County)
                .HasColumnName("County")
                .HasMaxLength(200)
                .IsUnicode();
            addressBuilder.Property(a => a.DetailAddress)
                .HasColumnName("DetailAddress")
                .HasMaxLength(512)
                .IsUnicode();
            addressBuilder.Property(a => a.Latitude)
                .HasColumnName("Latitude")
                .HasDefaultValue(0);
            addressBuilder.Property(a => a.Longitude)
                .HasColumnName("Longitude")
                .HasDefaultValue(0);

            var navigation = builder.Metadata
                .FindNavigation(nameof(Domain.AggregatesModel.ActivityAggregate.Activity.Attendees));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(a => a.ActivityStatus)
                .WithMany()
                .HasForeignKey("ActivityStatusId")
                .IsRequired();

            builder.HasOne(a => a.AddressVisibleRule)
                .WithMany()
                .HasForeignKey("AddressVisibleRuleId")
                .IsRequired();

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey("CategoryId")
                .IsRequired();
        }
    }
}
