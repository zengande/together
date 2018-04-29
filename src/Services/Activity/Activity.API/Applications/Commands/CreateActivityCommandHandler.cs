using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.API.Applications.Commands
{
    public class CreateActivityCommandHandler
        : IRequestHandler<CreateActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMediator _mediator;
        public CreateActivityCommandHandler(IActivityRepository activityRepository,
            IMediator mediator)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<bool> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = new Domain.AggregatesModel.ActivityAggregate.Activity(request.Owner.UserId,
                request.Description,
                request.Details,
                request.EndTime,
                request.ActivityTime,
                request.Address,
                request.LimitsNum);

            foreach (var participant in request.Participants)
            {
                activity.JoinActivity(participant.UserId, participant.Nickname, participant.Avatar);
            }

            _activityRepository.AddAsync(activity);

            return _activityRepository.UnitOfWork
                .SaveEntitiesAsync();
        }
    }
}
