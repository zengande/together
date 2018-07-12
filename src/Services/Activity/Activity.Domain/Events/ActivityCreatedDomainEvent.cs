using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Domain.Events
{
    public class ActivityCreatedDomainEvent
        : INotification
    {
        public AggregatesModel.ActivityAggregate.Activity Activity { get; set; }
    }
}
