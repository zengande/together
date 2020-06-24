using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Application.Elasticsearch;
using Together.Activity.Application.Elasticsearch.Models;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Application.Commands
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, int>
    {
        private readonly IActivityRepository _repository;
        private readonly ActivityIndexService _indexService;
        public CreateActivityCommandHandler(IActivityRepository repository,
            ActivityIndexService indexService)
        {
            _repository = repository;
            _indexService = indexService;
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
                request.CatalogId,
                request.AddressVisibleRuleId,
                request.LimitsNum);

            var entity = _repository.Add(activity);
            var result = await _repository.UnitOfWork.SaveEntitiesAsync();
            if (result)
            {
                var index = new ActivityIndex(entity.Id, entity.Title, entity.Content, entity.CreateTime, entity.EndRegisterTime, entity.ActivityStartTime, entity.ActivityEndTime, entity.Address.City, entity.Address.County, entity.Address.DetailAddress, entity.Address.Latitude, entity.Address.Longitude, entity.CatalogId, request.Creator.UserId, request.Creator.Nickname);
                _indexService.CreateIndex(index);
            }

            return result ? entity.Id : 0;
        }
    }
}
