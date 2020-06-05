using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Application.Commands
{
    public class JoinActivityCommandHandler : IRequestHandler<JoinActivityCommand, bool>
    {
        private readonly IActivityRepository _repository;
        public JoinActivityCommandHandler(IActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(JoinActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _repository.GetByIdAsync(request.ActivityId);
            if (activity != null)
            {
                activity.JoinActivity(request.UserInfo.Id, request.UserInfo.Nickname, request.UserInfo.Avatar, request.UserInfo.Gender);
                var result = await _repository.UnitOfWork.SaveEntitiesAsync();
                return result;
            }
            return false;
        }
    }
}
