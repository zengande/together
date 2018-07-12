using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.Exceptions;

namespace Together.Activity.API.Applications.Commands
{
    public class JoinActivityCommandHandler
        : IRequestHandler<JoinActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        public JoinActivityCommandHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<bool> Handle(JoinActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _activityRepository.GetAsync(request.ActivityId);
            if (activity == null)
            {
                throw new KeyNotFoundException($"未找到Id为{request.ActivityId}的活动");
            }

            activity.JoinActivity(request.User.UserId, request.User.Nickname, request.User.Avatar, request.User.Sex);
            return await _activityRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
