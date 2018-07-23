using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Applications.IntegrationEvents.Events;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.API.Applications.IntegrationEvents.EventHandlers
{
    public class ActivityExpiredIntegrationEventHandler
        : ICapSubscribe
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityExpiredIntegrationEventHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [CapSubscribe("Together.Activity.BackgroundTasks.ActivityExpired")]
        public async Task ConsumeAsync(ActivityExpiredIntegrationEvent message)
        {
            var activity = await _activityRepository.GetAsync(message.ActivityId);
            if (activity != null)
            {
                activity.SetFinishedStatus();
                await _activityRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }
    }
}
