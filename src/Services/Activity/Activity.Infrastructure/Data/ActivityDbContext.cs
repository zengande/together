using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
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
        //private readonly IConfiguration _configuration;
        //public ActivityDbContextDesignFactory(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public ActivityDbContext CreateDbContext(string[] args)
        {
            var connectionString = "User ID=postgres;Password=Pass@word;Host=127.0.0.1;Port=5432;Database=Together.ActivityDb;Pooling=true";
            //_configuration.GetValue<string>("ConnectionString") ??
            //    throw new ArgumentNullException("ConnectionString");
            var optionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>()
                .UseNpgsql(connectionString);

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
