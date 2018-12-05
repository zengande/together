using MediatR;
using System;
using System.Collections.Generic;
using Together.Activity.API.Applications.Dtos;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Extensions.Claims;

namespace Together.Activity.API.Applications.Commands
{
    public class CreateActivityCommand
        : IRequest<bool>
    {
        private readonly List<ParticipantDto> _participants;
        public IEnumerable<ParticipantDto> Participants => _participants;

        /// <summary>
        /// 活动发起人
        /// </summary>
        public CurrentUserInfo Owner { get; set; }

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
        /// 活动开始时间
        /// </summary>
        public DateTime ActivityStartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime ActivityEndTime { get; set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; set; }

        /// <summary>
        /// 活动类别
        /// </summary>
        public int CategoryId { get; set; }

        public ActivityAddress Address { get; set; }

        public int AddressVisibleRuleId { get; set; }

        public CreateActivityCommand()
        {
            _participants = new List<ParticipantDto>();
        }

        public CreateActivityCommand(CurrentUserInfo owner, string title, string details, DateTime endRegisterTime, DateTime startTime, DateTime endTime, ActivityAddress address, int categoryId, int? limitsNum = null)
            : this()
        {
            Owner = owner;
            Title = title;
            Details = details;
            RegisterEndTime = endRegisterTime;
            ActivityStartTime = startTime;
            ActivityEndTime = endTime;
            Address = address;
            LimitsNum = limitsNum;
            CategoryId = categoryId;
        }

        public Domain.AggregatesModel.ActivityAggregate.Activity ToActivityEntity()
        {
            if (Owner == null)
            {
                return null;
            }
            if (Address == null)
            {
                Address = new ActivityAddress();
            }
            var address = new Address(Address.Province, Address.City, Address.Detail, Address.Longitude, Address.Latitude);
            return new Domain.AggregatesModel.ActivityAggregate.Activity(Owner.UserId, Title, Details, RegisterEndTime, ActivityStartTime, ActivityEndTime, address, CategoryId, AddressVisibleRule.From(AddressVisibleRuleId), LimitsNum);
        }
    }

    public class ActivityAddress
    {
        public string Province { get; set; }
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
