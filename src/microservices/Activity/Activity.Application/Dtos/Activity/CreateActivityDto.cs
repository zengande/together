using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.Dtos.Activity
{
    public class CreateActivityDto
    {
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 截止报名时间
        /// </summary>
        public DateTime EndRegisterTime { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime ActivityStartTime { get; set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime ActivityEndTime { get; set; }
    }
}
