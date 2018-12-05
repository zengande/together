using DotNetCore.CAP;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Applications.IntegrationEvents.Events;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.API.Applications.IntegrationEvents.EventHandlers
{
    public class ActivityRecruitmentCompletedIntegrationEventHandler
        : ICapSubscribe
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityRecruitmentCompletedIntegrationEventHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [CapSubscribe("Together.Activity.BackgroundTasks.ActivityRecruitmentCompleted")]
        public async Task ConsumeAsync(ActivityRecruitmentCompletedIntegrationEvent message)
        {
            var activities = await _activityRepository.FindListAsync(message.ActivityIds);
            if (activities != null)
            {
                activities.ToList().ForEach(a => a.SetProcessingStatus());
                await _activityRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }
    }
}
