using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Messaging.Application.IntegrationEvents
{
    public class NewUserJoinedActivityIntegrationEvent
    {
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string OwnerId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime JoinedTimeUtc { get; set; }
    }
}
