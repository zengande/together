using System.Collections.Generic;

namespace Together.Activity.BackgroundTasks.IntegrationEvents.Events
{
    public class ActivityRecruitmentCompletedIntegrationEvent
    {
        public ActivityRecruitmentCompletedIntegrationEvent(List<int> activityIds) => ActivityIds = activityIds;

        public List<int> ActivityIds { get; private set; }
    }
}
