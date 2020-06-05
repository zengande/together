using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.Events
{
    public class UserJoinedActivityDomainEvent : DomainEvent
    {
        public Participant Participant { get; }
        public UserJoinedActivityDomainEvent(Participant participant)
        {
            Participant = participant;
        }
    }
}
