using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure.Dtos;
using WebMVC.ViewModels.Activity;

namespace WebMVC.Services
{
    public interface IActivityService
    {
        Task<bool> CreateActivity(ActivityCreateDto dto);

        Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesNearbyAsync(string location);
        Task<ActivityDetailViewModel> GetActivityAsync(int id);

        Task<bool> Join(int activityId);

        Task<IList<ActivitySearchResultDto>> SearchAsync(string keyword, int offset = 0, int limit = 10);
    }
}
