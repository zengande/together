using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Infrastructure.Data
{
    public class UserGroupDbContext
        : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public UserGroupDbContext(DbContextOptions<UserGroupDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserModelBuildConfiguration(modelBuilder.Entity<User>());
            GroupModelBuildConfiguration(modelBuilder.Entity<Group>());
        }

        private void GroupModelBuildConfiguration(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");

            builder.HasKey(g => g.Id);
            builder.Property(g => g.GroupName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(g => g.Introduction)
                .HasMaxLength(200);
            builder.Property(g => g.CreateDate)
                .IsRequired();
            builder.Property(g => g.Founder)
                .HasMaxLength(50);

            var navigation = builder.Metadata
              .FindNavigation(nameof(Group.Members));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void UserModelBuildConfiguration(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nickname)
                .HasMaxLength(50);
            builder.Property(u => u.Avatar)
                .HasMaxLength(200);
            builder.Property(u => u.Sex)
                .IsRequired(false);
        }
    }
}
