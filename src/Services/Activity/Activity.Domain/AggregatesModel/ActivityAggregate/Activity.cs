using System;
using System.Collections.Generic;
using System.Linq;
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

        public AddressVisibleRule AddressVisibleRule { get; private set; }
        private int _addressVisibleRuleId;

        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; private set; }

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
        public DateTime EndRegisterTime { get; private set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime ActivityStartTime { get; private set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        public int? LimitsNum { get; private set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime ActivityEndTime { get; private set; }

        /// <summary>
        /// 活动类别
        /// </summary>
        public int? CategoryId { get; private set; }

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
        public Activity(string userId, string description, string details, DateTime endRegisterTime, DateTime startTime, DateTime endTime, Address address, int categoryId, AddressVisibleRule addressVisibleRule, int? limitsNum = null)
            : this()
        {
            // 截止报名时间早于当前时间（活动在过去）
            if (DateTimeOffset.Now > endRegisterTime)
            {
                throw new ActivityDomainException("截止报名时间不能早于当前时间");
            }
            // 活动时间早于截止报名时间
            if (startTime < endRegisterTime)
            {
                throw new ActivityDomainException("截止报名时间不能晚于活动时间");
            }
            // 开始时间晚于结束时间
            if (startTime > endTime)
            {
                throw new ActivityDomainException("开始时间不能晚于结束结束时间");
            }
            OwnerId = userId;
            Title = description;
            Details = details;
            EndRegisterTime = endRegisterTime;
            ActivityStartTime = startTime;
            ActivityEndTime = endTime;
            Address = address;
            LimitsNum = limitsNum;
            CategoryId = categoryId;
            _activityStatusId = ActivityStatus.Recruitment.Id;
            if (addressVisibleRule == null)
            {
                addressVisibleRule = AddressVisibleRule.PublicVisible;
            }
            _addressVisibleRuleId = addressVisibleRule.Id;
        }

        /// <summary>
        /// 加入活动
        /// </summary>
        public void JoinActivity(string userId, string nickname, string avatar, int sex)
        {
            // 非活动所有者
            if (OwnerId != userId)
            {
                // 招募状态才能加入
                if (_activityStatusId != ActivityAggregate.ActivityStatus.Recruitment.Id)
                {
                    throw new ActivityDomainException($"该活动当前状态不允许加入");
                }

                // 已参加了此活动
                if (_participants.Any(u => u.UserId == userId))
                {
                    return;
                }

                // 判断是否已经截止了报名
                if (DateTimeOffset.Now > EndRegisterTime)
                {
                    throw new ActivityDomainException("已经截止了报名");
                }


                // 人数已满
                if (LimitsNum.HasValue)
                {
                    // 除本人外人数限制
                    if (_participants.Count > LimitsNum.Value)
                    {
                        throw new ActivityDomainException("本次活动人数已满");
                    }
                }
            }

            var participant = new Participant(userId, nickname, avatar, sex, userId == OwnerId);
            _participants.Add(participant);

            // 加入活动领域事件
            AddDomainEvent(new UserJoinedActivityDomainEvent { Participant = participant });
        }

        public void SetFinishedStatus()
        {
            if (_activityStatusId == ActivityStatus.Processing.Id)
            {
                _activityStatusId = ActivityStatus.Finished.Id;
            }
        }

        public void SetProcessingStatus()
        {
            if (_activityStatusId == ActivityStatus.Recruitment.Id)
            {
                _activityStatusId = ActivityStatus.Processing.Id;
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        public void Obsolete()
        {
            if (_activityStatusId == ActivityStatus.Recruitment.Id)
            {
                _activityStatusId = ActivityStatus.Obsoleted.Id;
            }
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
