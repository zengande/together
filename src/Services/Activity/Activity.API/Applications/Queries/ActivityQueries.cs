using System.Collections.Generic;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.Queries
{
    using System;
    using Together.Activity.API.Applications.Dtos;

    public class ActivityQueries
        : DapperQueries, IActivityQueries
    {
        public ActivityQueries(string constr) : base(constr) { }

        public async Task<IEnumerable<ActivitySummaryDto>> GetActivitiesAsync(int pageIndex, int pageSize, int status = 2)
        {
            var sql = "SELECT \"a\".\"Id\",\"a\".\"OwnerId\",\"a\".\"Title\",\"a\".\"ActivityStartTime\",\"a\".\"LimitsNum\",b.\"Nickname\",b.\"Avatar\" FROM activities a LEFT JOIN participant b on a.\"Id\"=b.\"ActivityId\" WHERE \"ActivityStatusId\"=@status AND a.\"OwnerId\"=b.\"UserId\" ORDER BY \"CreateTime\" DESC LIMIT @limit OFFSET @offset";
            return await SqlQuery<ActivitySummaryDto>(sql, new { limit = pageSize, offset = (pageIndex - 1) * pageSize, status });
        }

        public async Task<IEnumerable<ActivitySummaryDto>> GetLatestActivitiesNearby(int pageIndex, int pageSize, string location)
        {
            var sql = "SELECT \"a\".\"Id\",\"a\".\"OwnerId\",\"a\".\"Title\",\"a\".\"ActivityStartTime\",\"a\".\"LimitsNum\",b.\"Nickname\",b.\"Avatar\" FROM activities a LEFT JOIN participant b on a.\"Id\"=b.\"ActivityId\" WHERE \"ActivityStatusId\" IN (1,2) AND a.\"OwnerId\"=b.\"UserId\" ORDER BY \"CreateTime\" DESC LIMIT @limit OFFSET @offset";
            return await SqlQuery<ActivitySummaryDto>(sql, new { limit = pageSize, offset = (pageIndex - 1) * pageSize });
        }

        public async Task<ActivityDetailDto> GetActivityAsync(int id)
        {
            var sql = "SELECT * FROM activities WHERE \"Id\" = @Id";

            var result = await SqlQueryFirstOrDefaultAsync<ActivityDetailDto>(sql, new { id });
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return await MapActivityAndParticipant(result);
        }

        public Task<IEnumerable<ActivitySummaryDto>> GetActivitiesByUserAsync(string userId)
        {
            return null;
        }


        public async Task<bool> AlreadyJoined(int activityId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            var sql = "SELECT COUNT(*) FROM participant WHERE \"ActivityId\"=@activityId AND \"UserId\"=@userId";
            var result = await SqlQueryFirstOrDefaultAsync<int>(sql, new { activityId, userId });
            return result > 0;
        }

        private async Task<ActivityDetailDto> MapActivityAndParticipant(ActivityDetailDto vm)
        {
            vm.NumberOfParticipants = await GetNumberOfParticipantsAsync(vm.Id);

            vm.Participants = await GetTopParticipantsAsync(vm.Id);

            return vm;
        }

        private async Task<int> GetNumberOfParticipantsAsync(int activityId)
        {
            var sql = "SELECT COUNT(*) FROM participant WHERE \"ActivityId\"=@activityId";
            return await SqlQueryFirstOrDefaultAsync<int>(sql, new { activityId });
        }

        private async Task<IEnumerable<ParticipantDto>> GetTopParticipantsAsync(int activityId)
        {
            var sql = "SELECT \"UserId\",\"Nickname\",\"Avatar\",\"Sex\",\"JoinTime\", \"IsOwner\" FROM participant WHERE \"ActivityId\"=@activityId ORDER BY \"JoinTime\" LIMIT 10";

            return await SqlQuery<ParticipantDto>(sql, new { activityId });
        }
    }


}
