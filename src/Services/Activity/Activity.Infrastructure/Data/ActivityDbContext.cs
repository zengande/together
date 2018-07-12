using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.SeedWork;
using Together.Activity.Infrastructure.EntityTypeConfigurations;

namespace Together.Activity.Infrastructure.Data
{
    public class ActivityDbContext
        : DbContext, IUnitOfWork
    {
        public DbSet<Domain.AggregatesModel.ActivityAggregate.Activity> Activities { get; set; }
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public DbSet<Participant> Participants { get; set; }
        //public DbSet<Address> Addresses { get; set; }

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
            //modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync();
            return true;
        }
    }

    public class ActivityDbContextDesignFactory : IDesignTimeDbContextFactory<ActivityDbContext>
    {
        public ActivityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>()
                .UseSqlServer("Data Source=.;Initial Catalog=Together.ActivityDb;Persist Security Info=True;User ID=sa;Password=pwd123");

            return new ActivityDbContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
        }
    }
}
