using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.AggregatesModel.CategoryAggregate;
using Together.Activity.Domain.SeedWork;
using Together.Activity.Infrastructure.EntityTypeConfigurations;
using Together.Activity.Infrastructure.Idempotency;

namespace Together.Activity.Infrastructure.Data
{
    public class ActivityDbContext
        : DbContext, IUnitOfWork
    {
        public DbSet<Domain.AggregatesModel.ActivityAggregate.Activity> Activities { get; set; }
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public DbSet<AddressVisibleRule> AddressVisibleRules { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ClientRequest> ClientRequests { get; set; }
        public DbSet<Category> Categories { get; set; }

        private readonly IMediator _mediator;

        public ActivityDbContext(DbContextOptions<ActivityDbContext> options)
            : base(options)
        {
        }

        public ActivityDbContext(DbContextOptions<ActivityDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AddressVisibleRuleEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}
