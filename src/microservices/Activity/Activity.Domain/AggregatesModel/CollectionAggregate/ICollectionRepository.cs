using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.CollectionAggregate
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Task<Collection> GetAsync(int activityId, string userId);
    }
}
