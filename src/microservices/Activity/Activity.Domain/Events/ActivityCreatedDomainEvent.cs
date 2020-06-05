using System;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.Events
{
    public class ActivityCreatedDomainEvent : DomainEvent
    {
        public AggregatesModel.ActivityAggregate.Activity Activity { get;  }
        public ActivityCreatedDomainEvent(AggregatesModel.ActivityAggregate.Activity activity)
        {
            Activity = activity;
        }
    }
}
