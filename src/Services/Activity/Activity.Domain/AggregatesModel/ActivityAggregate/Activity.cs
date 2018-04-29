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
        public int? OwnerId { get; private set; }

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
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime ActivityTime { get; private set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; private set; }

        /// <summary>
        /// 参与者
        /// </summary>
        private readonly List<Participant> _participants;
        public IReadOnlyCollection<Participant> Participants => _participants;

        protected Activity()
        {
            _participants = new List<Participant>();
            CreateTime = DateTime.Now;

            // 创建活动领域事件
            AddDomainEvent(new ActivityCreatedEvent { Activity = this });
        }

        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="discription"></param>
        /// <param name="details"></param>
        /// <param name="endTime"></param>
        /// <param name="activityTime"></param>
        /// <param name="address"></param>
        /// <param name="limitsNum"></param>
        public Activity(int? userId, string description, string details, DateTime endTime, DateTime activityTime, string address, int? limitsNum = null)
            : this()
        {
            OwnerId = userId;
            Description = description;
            Details = details;
            EndTime = endTime;
            ActivityTime = activityTime;
            Address = address;
            LimitsNum = limitsNum;
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
        public void JoinActivity(int userId, string nickname, string avatar)
        {
            // 已参加了此活动
            if (_participants.Any(u => u.UserId == userId))
            {
                return;
            }
            // 人数已满
            if (LimitsNum.HasValue)
            {
                if (_participants.Count >= LimitsNum.Value)
                {
                    JoinActivityException();
                }
            }

            var participant = new Participant(userId, nickname, avatar);
            _participants.Add(participant);

            // 加入活动领域事件
            AddDomainEvent(new ActivityJoinedEvent { Participant = participant });
        }

        /// <summary>
        /// 改变活动状态异常
        /// </summary>
        /// <param name="activityStatusToChange"></param>
        private void StatusChangeException(ActivityStatus activityStatusToChange)
        {
            throw new ActivityDomainException($"Is not possible to change the activity status from {ActivityStatus.Name} to {activityStatusToChange.Name}.");
        }

        /// <summary>
        /// 加入活动人数已满异常
        /// </summary>
        private void JoinActivityException()
        {
            throw new ActivityDomainException($"The number of people involved in this activity has reached its maximum:{LimitsNum.Value}");
        }
    }
}
