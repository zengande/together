using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Domain.Events
{
    public class ActivityJoinedEvent
        : INotification
    {
        public AggregatesModel.ActivityAggregate.Participant Participant { get; set; }
    }
}
