using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MediatR;
using Together.Activity.API.Models;
using Together.Activity.API.Extensions;

namespace Together.Activity.API.Applications.Commands
{
    public class CreateActivityCommand
        : IRequest<bool>
    {
        [DataMember]
        private readonly List<ParticipantDto> _participants;
        [DataMember]
        public IEnumerable<ParticipantDto> Participants => _participants;

        /// <summary>
        /// 活动发起人
        /// </summary>
        [DataMember]
        public CurrentUser Owner { get; private set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [DataMember]
        public string Description { get; private set; }

        /// <summary>
        /// 活动详情
        /// </summary>
        [DataMember]
        public string Details { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 截止报名时间
        /// </summary>
        [DataMember]
        public DateTime EndRegisterTime { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 活动时间
        /// </summary>
        [DataMember]
        public DateTime ActivityDate { get; private set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        [DataMember]
        public string Address { get; private set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        [DataMember]
        public int? LimitsNum { get; private set; }

        /// <summary>
        /// 活动经费
        /// </summary>
        [DataMember]
        public decimal? Funds { get; set; }

        public CreateActivityCommand()
        {
            _participants = new List<ParticipantDto>();
        }

        public CreateActivityCommand(CurrentUser owner, string description, string details, DateTime endRegisterTime, DateTime activityDate, DateTime startTime, DateTime endTime, string address, int? limitsNum = null)
            : this()
        {
            Owner = owner;
            Description = description;
            Details = details;
            EndRegisterTime = endRegisterTime;
            ActivityDate = activityDate;
            StartTime = startTime;
            EndTime = endTime;
            Address = address;
            LimitsNum = limitsNum;
        }

    }

    public class ParticipantDto
    {
        public int Sex { get; set; }
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
    }
}
