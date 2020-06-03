using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Application.Commands
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, int>
    {
        private readonly IActivityRepository _repository;
        public CreateActivityCommandHandler(IActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = new Domain.AggregatesModel.ActivityAggregate.Activity(
                request.Creator,
                request.Title,
                request.Content,
                request.EndRegisterTime,
                request.ActivityStartTime,
                request.ActivityEndTime,
                request.Address,
                0,
                request.AddressVisibleRuleId,
                request.LimitsNum);

            var entity = _repository.Add(activity);
            var result = await _repository.UnitOfWork.SaveEntitiesAsync();

            return result ? entity.Id : 0;
        }
    }
}
