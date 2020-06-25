using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.Events
{
    public class UserJoinedActivityDomainEvent : DomainEvent
    {
        public Attendee Attendee { get; }
        public UserJoinedActivityDomainEvent(Attendee attendee)
        {
            Attendee = attendee;
        }
    }
}
