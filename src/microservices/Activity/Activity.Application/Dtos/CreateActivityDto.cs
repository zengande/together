using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.Dtos
{
    public class CreateActivityDto
    {
        public CreateActivityDto()
        {
            Address = new ActivityAddress();
        }
        
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

        /// <summary>
        /// 地址可见规则
        /// </summary>
        public int AddressVisibleRuleId { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// 活动地址
        /// </summary>
        public ActivityAddress Address { get; set; }
    }

    public class ActivityAddress
    {
        public string City { get; set; }
        public string County { get; set; }
        public string Detail { get; set; }
        public int CityCode { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}