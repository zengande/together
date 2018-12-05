using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.API.Applications.IntegrationEvents.Events;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Idempotency;

namespace Together.Activity.API.Applications.Commands
{
    public class CreateActivityCommandHandler
        : IRequestHandler<CreateActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMediator _mediator;
        private readonly ICapPublisher _publisher;
        public CreateActivityCommandHandler(IActivityRepository activityRepository,
            IMediator mediator,
            ICapPublisher publisher)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _publisher = publisher;
        }

        public async Task<bool> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = request.ToActivityEntity();
            if (activity == null)
            {
                return false;
            }

            var owner = request.Owner;
            activity.JoinActivity(owner.UserId, owner.Nickname, owner.Avatar, owner.Gender);

            await _activityRepository.AddAsync(activity);

            var result = await _activityRepository.UnitOfWork
                .SaveEntitiesAsync();

            if (result)
            {
                await _publisher.PublishAsync("Together.Searching.NewActivityCreated", new NewActivityCreatedIntegrationEvent(activity.Id, activity.Title, activity.Details, activity.CreateTime));
            }

            return result;
        }
    }

    public class CreateActivityIdentifiedCommandHandler
        : IdentifiedCommandHandler<CreateActivityCommand, bool>
    {
        public CreateActivityIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, ILogger<IdentifiedCommandHandler<CreateActivityCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return false;
        }
    }
}
