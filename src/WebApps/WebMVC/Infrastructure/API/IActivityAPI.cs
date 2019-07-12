using Refit;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Infrastructure.Dtos;

namespace WebMVC.Infrastructure.API
{
    public interface IActivityAPI
    {
        [Get("/api/activities/nearby")]
        Task<HttpResponseMessage> GetActivitiesAsync(string location);

        [Get("/api/activities/{activityId}")]
        Task<HttpResponseMessage> GetActivityAsync(int activityId);

        [Post("/api/activities/create")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> CreateActivity([Body]ActivityCreateDto dto);

        [Put("/api/activities/join/{activityId}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> Join(int activityId);

        [Get("/api/v1/search/activities?keyword={keyword}&offset={offset}&limit={limit}")]
        Task<HttpResponseMessage> SearchAsync(string keyword, int offset = 0, int limit = 10);
    }
}
