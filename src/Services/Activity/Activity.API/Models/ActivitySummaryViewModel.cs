﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Models
{
    public class ActivitySummaryViewModel
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NumberOfParticipants { get; set; }
        public int? LimitsNum { get; set; }
        //public string Address { get; set; }
    }
}
