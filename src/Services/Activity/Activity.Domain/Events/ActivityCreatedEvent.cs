using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Domain.Events
{
    public class ActivityCreatedEvent
        : INotification
    {
        public AggregatesModel.ActivityAggregate.Activity Activity { get; set; }
    }
}
