using System;
using WebMVC.Infrastructure.Enums;

namespace WebMVC.Infrastructure.Dtos
{
    public class ActivityCreateDto
    {
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 活动详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 截止报名时间
        /// </summary>
        public DateTime RegisterEndTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime ActivityStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ActivityEndTime { get; set; }

        /// <summary>
        /// 活动地址
        /// </summary>
        //[Required]
        //public string Address { get; set; }

        public ActivityAddressDto Address { get; set; }
        public int AddressVisibleRuleId { get; set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; set; }

        /// <summary>
        /// 参与经费
        /// </summary>
        public decimal? Funds { get; set; }

        public int CategoryId { get; set; }
    }
}
