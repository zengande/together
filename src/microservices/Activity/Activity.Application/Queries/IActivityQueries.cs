using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos.Activity;

namespace Together.Activity.Application.Queries
{
    public interface IActivityQueries
    {
        Task<ActivityDto> GetActivityByIdAsync(int id);
    }
}
