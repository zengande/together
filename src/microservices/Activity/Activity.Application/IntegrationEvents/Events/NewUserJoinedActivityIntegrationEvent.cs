using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.IntegrationEvents.Events
{
    public class NewUserJoinedActivityIntegrationEvent
    {
        public int ActivityId { get; set; }
        public string Title { get; private set; }
        public string OwnerId { get; private set; }
        public string UserId { get; private set; }
        public string UserName { get; set; }
        public DateTime JoinedTimeUtc { get; private set; }
        public NewUserJoinedActivityIntegrationEvent(int activityId, string title, string ownerId, string userId, string userName)
        {
            ActivityId = activityId;
            Title = title;
            OwnerId = ownerId;
            UserId = userId;
            UserName = userName;
            JoinedTimeUtc = DateTime.UtcNow;
        }
    }
}
