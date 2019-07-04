using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Infrastructure.API;
using WebMVC.Infrastructure.Dtos;
using WebMVC.ViewModels.Activity;

namespace WebMVC.Services
{
    public class ActivityService
        : IActivityService
    {
        private readonly IActivityAPI _activityAPI;
        private readonly ILogger<ActivityService> _logger;
        public ActivityService(IActivityAPI activityAPI,
            ILogger<ActivityService> logger)
        {
            _activityAPI = activityAPI;
            _logger = logger;
        }

        public async Task<bool> CreateActivity(ActivityCreateDto dto)
        {
            var response = await _activityAPI.CreateActivity(dto);
            if (response.IsSuccessStatusCode == false)
            {
                await LogError(response);
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesNearbyAsync(string location)
        {
            var response = await _activityAPI.GetActivitiesAsync(location);
            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<ActivitySummaryViewModel>>(json);
        }

        public async Task<ActivityDetailViewModel> GetActivityAsync(int id)
        {
            var response = await _activityAPI.GetActivityAsync(id);
            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ActivityDetailViewModel>(json);
        }

        public async Task<bool> Join(int activityId)
        {
            var response = await _activityAPI.Join(activityId);
            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                return false;
            }
            return true;
        }

        public async Task<IList<ActivitySearchResultDto>> SearchAsync(string keyword, int offset = 0, int limit = 10)
        {
            var response = await _activityAPI.SearchAsync(keyword, offset, limit);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json) == false)
                {
                    return JsonConvert.DeserializeObject<IList<ActivitySearchResultDto>>(json);
                }
            }
            return null;
        }

        private async Task LogError(HttpResponseMessage response)
        {
            _logger.LogError($"{response.RequestMessage.Method} {response.RequestMessage.RequestUri} => {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
        }
    }
}
