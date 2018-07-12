using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.Activity.Domain.Events;
using Together.Activity.Domain.Exceptions;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class Activity
        : Entity, IAggregateRoot
    {
        /// <summary>
        /// 活动发起人
        /// </summary>
        public string OwnerId { get; private set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityStatus ActivityStatus { get; private set; }
        private int _activityStatusId;

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 活动详情
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 截止报名时间
        /// </summary>
        public DateTime EndRegisterDate { get; private set; }

        /// <summary>
        /// 活动日期
        /// </summary>
        public DateTime ActivitDate { get; private set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; private set; }

        /// <summary>
        /// 参加经费
        /// </summary>
        public decimal? Funds { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; private set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// 参与者
        /// </summary>
        private readonly List<Participant> _participants;
        public IReadOnlyCollection<Participant> Participants => _participants;

        protected Activity()
        {
            _participants = new List<Participant>();
            CreateTime = DateTimeOffset.Now.DateTime;

            // 创建活动领域事件
            AddDomainEvent(new ActivityCreatedDomainEvent { Activity = this });
        }

        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="discription"></param>
        /// <param name="details"></param>
        /// <param name="endRegisterTime"></param>
        /// <param name="activityDate"></param>
        /// <param name="address"></param>
        /// <param name="limitsNum"></param>
        public Activity(string userId, string description, string details, DateTime endRegisterTime, DateTime activityDate, DateTime startTime, DateTime endTime, Address address, int? limitsNum = null, decimal? funds = null)
            : this()
        {
            // 截止报名时间早于当前时间（活动在过去）
            if (DateTimeOffset.Now > endRegisterTime)
            {
                throw new ActivityDomainException("截止报名时间不能早于当前时间");
            }
            // 活动时间早于截止报名时间
            if (activityDate < endRegisterTime)
            {
                throw new ActivityDomainException("截止报名时间不能晚于活动时间");
            }
            // 开始时间晚于结束时间
            if (startTime > endTime)
            {
                throw new ActivityDomainException("开始时间不能晚于结束结束时间");
            }
            OwnerId = userId;
            Description = description;
            Details = details;
            EndRegisterDate = endRegisterTime;
            ActivitDate = activityDate;
            StartTime = startTime;
            EndTime = endTime;
            Address = address;
            LimitsNum = limitsNum;
            Funds = funds;
            _activityStatusId = ActivityStatus.Draft.Id;
        }

        /// <summary>
        /// 提交活动
        /// </summary>
        public void SubmitActivity()
        {
            if (_activityStatusId != ActivityStatus.Draft.Id)
            {
                StatusChangeException(ActivityStatus.Normal);
            }
            _activityStatusId = ActivityStatus.Normal.Id;
        }

        /// <summary>
        /// 加入活动
        /// </summary>
        public void JoinActivity(string userId, string nickname, string avatar, int sex)
        {
            // 已参加了此活动
            if (_participants.Any(u => u.UserId == userId))
            {
                return;
            }

            // 判断是否已经截止了报名
            if (DateTimeOffset.Now > EndRegisterDate)
            {
                throw new ActivityDomainException("已经截止了报名");
            }

            // 人数已满
            if (LimitsNum.HasValue)
            {
                if (_participants.Count >= LimitsNum.Value)
                {
                    throw new ActivityDomainException("本次活动人数已满");
                }
            }

            var participant = new Participant(userId, nickname, avatar, sex);
            _participants.Add(participant);

            // 加入活动领域事件
            AddDomainEvent(new UserJoinedActivityDomainEvent { Participant = participant });
        }

        /// <summary>
        /// 改变活动状态异常
        /// </summary>
        /// <param name="activityStatusToChange"></param>
        private void StatusChangeException(ActivityStatus activityStatusToChange)
        {
            throw new ActivityDomainException($"Is not possible to change the activity status from {ActivityStatus.Name} to {activityStatusToChange.Name}.");
        }
    }
}
