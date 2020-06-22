using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.CollectionAggregate
{
    public class Collection : Entity, IAggregateRoot
    {
        public string UserId { get; private set; }
        public int ActivityId { get; private set; }
        public DateTime CollectionTimeUtc { get; private set; }

        protected Collection()
        {
            CollectionTimeUtc = DateTime.UtcNow;
        }

        public Collection(string userId, int activityId) : this()
        {
            UserId = !string.IsNullOrWhiteSpace(userId) ? userId : throw new ArgumentNullException(nameof(userId));
            ActivityId = activityId;
        }
    }
}
