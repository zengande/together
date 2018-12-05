using AutoMapper.Configuration.Conventions;
using System;
using System.ComponentModel.DataAnnotations;
using WebMVC.Infrastructure.Dtos;
using WebMVC.Infrastructure.Enums;

namespace WebMVC.ViewModels.Activity
{
    public class ActivityCreateViewModel
    {
        public ActivityCreateViewModel()
        {
            AddressVisibleRuleId = (int)AddressVisibleRules.PublicVisible;
            ActivityStartTime = GetCurrentDateString();
            ActivityEndTime = ActivityStartTime;
            RegisterEndTime = ActivityStartTime;
            Address = new ActivityAddressDto();
        }
        [Required]
        [MapTo("Title")]
        public string Title { get; set; }
        [Required]
        public int ParentCategory { get; set; }
        public int? ChildCategory { get; set; }
        public string Remarks { get; set; }
        [MapTo("Details")]
        [Required]
        public string Details { get; set; }

        public int AddressVisibleRuleId { get; set; }

        public ActivityAddressDto Address { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        [MapTo("ActivityStartTime")]
        public string ActivityStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        [MapTo("ActivityEndTime")]
        public string ActivityEndTime { get; set; }

        /// <summary>
        /// 截止报名时间
        /// </summary>
        [Required]
        [MapTo("RegisterEndTime")]
        public string RegisterEndTime { get; set; }

        [Required]
        public int CategoryId
        {
            get
            {
                if (ChildCategory.HasValue)
                {
                    return ChildCategory.Value;
                }
                return ParentCategory;
            }
        }

        private string GetCurrentDateString()
        {
            var now = DateTime.Now;
            if (now.Minute <= 30)
            {
                return $"{now.ToString("yyyy-MM-dd HH")}:30";
            }
            return $"{now.AddHours(1).ToString("yyyy-MM-dd HH")}:00";
        }
    }
}
