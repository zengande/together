using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Together.Activity.Domain.Events
{
    public class ActivityCreatedDomainEventHandler
        : INotificationHandler<ActivityCreatedDomainEvent>
    {
        public Task Handle(ActivityCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
