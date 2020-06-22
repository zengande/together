using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos;

namespace Together.Activity.Application.Queries
{
    public interface IActivityQueries
    {
        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActivityDto> GetActivityByIdAsync(int id, bool showAddress = false);
        /// <summary>
        /// 获取活动参与者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<ParticipantDto>> GetActivityParticipantsAsync(int id);
        /// <summary>
        /// 是否加入了此活动
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsJoinedAsync(int activityId, string userId);
    }
}
