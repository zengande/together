using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Together.Activity.Domain.AggregatesModel.CategoryAggregate;

namespace Together.Activity.Infrastructure.EntityTypeConfigurations
{
    public class CategoryEntityTypeConfiguration
        : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.DomainEvents);

            builder.Property(c => c.Id)
                .ForNpgsqlUseSequenceHiLo("categoryseq");

            builder.Property(c => c.Key)
                .HasMaxLength(150)
                .IsRequired(false);
            builder.Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode();
            builder.Property(c => c.Sort)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(c => c.Enabled)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(c => c.ParentId)
                .IsRequired(false);

            builder.Property(c => c.CoverImage)
                .HasMaxLength(512);

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
