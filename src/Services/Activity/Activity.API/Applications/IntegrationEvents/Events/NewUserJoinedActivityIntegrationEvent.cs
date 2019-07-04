using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.IntegrationEvents.Events
{
    public class NewUserJoinedActivityIntegrationEvent
    {
        public NewUserJoinedActivityIntegrationEvent(string ownerId,string activityTitle,string who)
        {
            OwnerId = ownerId;
            ActivityTitle = activityTitle;
            Who = who;
            JoinTime = DateTime.UtcNow;
        }
        public string OwnerId { get; private set; }
        public string ActivityTitle { get; private set; }
        public string Who { get; private set; }
        public DateTime JoinTime { get; private set; }
    }
}
