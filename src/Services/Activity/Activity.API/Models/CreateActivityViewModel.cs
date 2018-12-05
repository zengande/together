using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Models
{
    public class CreateActivityViewModel
    {
        /// <summary>
        /// 活动标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 活动详情
        /// </summary>
        [Required]
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

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; set; }

        /// <summary>
        /// 参与经费
        /// </summary>
        public decimal? Funds { get; set; }

        public int CategoryId { get; set; }

        public ActivityAddress ActivityAddress { get; set; }
    }

    public class ActivityAddress
    {
        public string City { get; set; }
        public string Detail { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
    }
}
