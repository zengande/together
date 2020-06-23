using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.CollectionAggregate;

namespace Together.Activity.Application.Commands
{
    public class CollectActivityCommandHandler : IRequestHandler<CollectActivityCommand, bool>
    {
        private readonly ICollectionRepository _repository;
        public CollectActivityCommandHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CollectActivityCommand request, CancellationToken cancellationToken)
        {
            // 已收藏
            if (await _repository.GetAsync(request.ActivityId, request.UserId) != null)
            {
                return true;
            }
            var collection = new Collection(request.UserId, request.ActivityId);
            _repository.Add(collection);
            var result = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;
        }
    }
}
