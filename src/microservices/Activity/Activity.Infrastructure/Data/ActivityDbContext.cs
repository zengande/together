using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.EntityTypeConfigurations;
using Together.BuildingBlocks.Infrastructure.Data;

namespace Together.Activity.Infrastructure.Data
{
    public class ActivityDbContext : DbContextBase
    {
        public const string DbSchema = null;
        public const string DbTablePrefix = "App";

        public DbSet<Domain.AggregatesModel.ActivityAggregate.Activity> Activities { get; set; }
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public DbSet<AddressVisibleRule> AddressVisibleRules { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public ActivityDbContext(DbContextOptions<ActivityDbContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AddressVisibleRuleEntityTypeConfiguration());
        }
    }

    public class ActivityDbContextDesignFactory : DbContextDesignFactoryBase<ActivityDbContext>
    {
        public override ActivityDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>()
                   .UseMySql(configuration.GetConnectionString("Default"));

            return new ActivityDbContext(optionsBuilder.Options, new NoMediator());
        }
    }
}
