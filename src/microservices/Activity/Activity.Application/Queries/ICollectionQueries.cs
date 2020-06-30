using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Together.Activity.Application.Queries
{
    public interface ICollectionQueries
    {
        Task<bool> IsCollected(int activityId, string userId);
    }
}
