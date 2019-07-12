﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Together.Activity.Domain.AggregatesModel.CategoryAggregate;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class ActivityEntityTypeConfiguration
        : IEntityTypeConfiguration<Domain.AggregatesModel.ActivityAggregate.Activity>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.ActivityAggregate.Activity> builder)
        {
            builder.ToTable("activities");

            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.DomainEvents);

            builder.Property(a => a.Id)
                .ForNpgsqlUseSequenceHiLo("activityseq");

            builder.Property(a => a.ActivityStartTime)
                .IsRequired();
            builder.Property(a => a.ActivityEndTime)
                .IsRequired();
            builder.Property(a => a.CreateTime)
                .IsRequired();
            builder.Property(a => a.OwnerId)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property<int>("ActivityStatusId")
                .IsRequired();
            builder.Property<int>("AddressVisibleRuleId")
                .IsRequired();
            builder.Property(a => a.Title)
                .IsRequired(false);
            builder.Property(a => a.Details)
                .IsRequired(false);
            builder.Property(a => a.EndRegisterTime)
                .IsRequired();
            builder.Property(a => a.CategoryId)
               .IsRequired(false);
            builder.Property(a => a.LimitsNum);

            builder.HasOne(a => a.ActivityStatus)
                .WithMany()
                .HasForeignKey("ActivityStatusId");

            builder.HasOne(a => a.AddressVisibleRule)
                .WithMany()
                .HasForeignKey("AddressVisibleRuleId");

            var addressBuilder = builder.OwnsOne(a => a.Address);
            addressBuilder.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();
            addressBuilder.Property(a => a.Province)
                .HasColumnName("Province")
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

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .IsRequired(false);

            var navigation = builder.Metadata
                .FindNavigation(nameof(Domain.AggregatesModel.ActivityAggregate.Activity.Participants));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
