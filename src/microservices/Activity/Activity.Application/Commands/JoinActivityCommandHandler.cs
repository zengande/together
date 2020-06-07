using DotNetCore.CAP;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Application.IntegrationEvents.Events;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Application.Commands
{
    public class JoinActivityCommandHandler : IRequestHandler<JoinActivityCommand, bool>
    {
        private readonly ICapPublisher _capBus;
        private readonly IActivityRepository _repository;
        public JoinActivityCommandHandler(IActivityRepository repository,
            ICapPublisher capBus)
        {
            _capBus = capBus;
            _repository = repository;
        }

        public async Task<bool> Handle(JoinActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _repository.GetByIdAsync(request.ActivityId);
            if (activity != null)
            {
                activity.JoinActivity(request.UserInfo.Id, request.UserInfo.Nickname, request.UserInfo.Avatar, request.UserInfo.Gender);
                var result = await _repository.UnitOfWork.SaveEntitiesAsync();
                if (result)
                {
                    // 发送有新加入的消息
                    var @event = new NewUserJoinedActivityIntegrationEvent(activity.Id, activity.Title, activity.CreatorId, request.UserInfo.Id, request.UserInfo.Nickname);
                    await _capBus.PublishAsync("Together.Messaging.Realtime.NewUserJoined", @event, cancellationToken: cancellationToken);
                }
                return result;
            }
            return false;
        }
    }
}
