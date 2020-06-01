using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.BuildingBlocks.Domain;

namespace Together.BuildingBlocks.Infrastructure.Data
{
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public DbContextBase(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        private Task DispatchDomainEventsAsync()
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x =>
                    x.Entity.DomainEvents != null &&
                    x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await _mediator.Publish(domainEvent);
                });

            return Task.WhenAll(tasks);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync();

            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
