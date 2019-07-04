using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.API.Applications.IntegrationEvents.Events;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Idempotency;

namespace Together.Activity.API.Applications.Commands
{
    public class JoinActivityCommandHandler
        : IRequestHandler<JoinActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICapPublisher _capPublisher;
        public JoinActivityCommandHandler(IActivityRepository activityRepository,
            ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
            _activityRepository = activityRepository;
        }

        public async Task<bool> Handle(JoinActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _activityRepository.GetAsync(request.ActivityId);
            if (activity == null)
            {
                throw new KeyNotFoundException($"未找到Id为{request.ActivityId}的活动");
            }

            activity.JoinActivity(request.User.UserId, request.User.Nickname, request.User.Avatar, request.User.Gender);
            var result = await _activityRepository.UnitOfWork.SaveEntitiesAsync();
            if (result)
            {
                var @event = new NewUserJoinedActivityIntegrationEvent(activity.OwnerId, activity.Title, request.User.Nickname);
                await _capPublisher.PublishAsync("Together.Notice.Realtime.NewUserJoined", @event);
            }
            return result;
        }
    }

    public class JoinActivityIdentifiedCommandHandler
        : IdentifiedCommandHandler<JoinActivityCommand, bool>
    {
        public JoinActivityIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, ILogger<IdentifiedCommandHandler<JoinActivityCommand, bool>> logger) : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return false;
        }
    }
}
