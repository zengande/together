using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class Activity
    {
        public int ActivityId { get; set; }

        /// <summary>
        /// 活动发起人
        /// </summary>
        public string OwnerId { get; private set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivityStatusId;

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 活动详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 截止报名时间
        /// </summary>
        public DateTime EndRegisterTime { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime ActivityStartTime { get; set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; set; }

        /// <summary>
        /// 参加经费
        /// </summary>
        public decimal? Funds { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime ActivityEndTime { get; set; }

        /// <summary>
        /// 活动类别
        /// </summary>
        public int CategoryId
        {
           get; set;
        }
    }
}
