using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.BuildingBlocks.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime CreatedAt { get; }
    }
}
