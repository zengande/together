using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.Activity.Domain.Events;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class Activity : Entity, IAggregateRoot
    {
        /// <summary>
        /// 活动发起人
        /// </summary>
        public string Creator { get; private set; }


        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; private set; }

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

        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivityStatusId { get; private set; }
        public ActivityStatus ActivityStatus { get; private set; }

        /// <summary>
        /// 地址可见规则
        /// </summary>
        public int AddressVisibleRuleId { get; private set; }
        public AddressVisibleRule AddressVisibleRule { get; private set; }

        protected Activity()
        {
            _participants = new List<Participant>();
            CreateTime = DateTimeOffset.UtcNow.DateTime;
        }

        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="endRegisterTime"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="address"></param>
        /// <param name="categoryId"></param>
        /// <param name="addressVisibleRule"></param>
        /// <param name="limitsNum"></param>
        public Activity(Participant creator, string title, string content, DateTime endRegisterTime, DateTime startTime, DateTime endTime, Address address, int categoryId, AddressVisibleRule addressVisibleRule, int? limitsNum = null)
            : this()
        {
            // 截止报名时间早于当前时间（活动在过去）
            if (DateTimeOffset.Now > endRegisterTime)
            {
                throw new DomainException("截止报名时间不能早于当前时间");
            }
            // 活动时间早于截止报名时间
            if (startTime < endRegisterTime)
            {
                throw new DomainException("截止报名时间不能晚于活动时间");
            }
            // 开始时间晚于结束时间
            if (startTime > endTime)
            {
                throw new DomainException("开始时间不能晚于结束结束时间");
            }
            Creator = creator.UserId;
            Title = title;
            Content = content;
            EndRegisterTime = endRegisterTime;
            ActivityStartTime = startTime;
            ActivityEndTime = endTime;
            Address = address;
            LimitsNum = limitsNum;
            CategoryId = categoryId;
            ActivityStatusId = ActivityStatus.Recruitment.Id;
            if (addressVisibleRule == null)
            {
                addressVisibleRule = AddressVisibleRule.PublicVisible;
            }
            AddressVisibleRuleId = addressVisibleRule.Id;

            _participants.Add(creator);

            // 创建活动领域事件
            AddDomainEvent(new ActivityCreatedDomainEvent(this));
        }

        /// <summary>
        /// 加入活动
        /// </summary>
        public void JoinActivity(string userId, string nickname, string avatar, int sex)
        {
            // 非活动所有者
            if (Creator != userId)
            {
                // 招募状态才能加入
                if (ActivityStatusId != ActivityAggregate.ActivityStatus.Recruitment.Id)
                {
                    throw new DomainException($"该活动当前状态不允许加入");
                }

                // 已参加了此活动
                if (_participants.Any(u => u.UserId == userId))
                {
                    return;
                }

                // 判断是否已经截止了报名
                if (DateTimeOffset.Now > EndRegisterTime)
                {
                    throw new DomainException("已经截止了报名");
                }


                // 人数已满
                if (LimitsNum.HasValue)
                {
                    // 除本人外人数限制
                    if (_participants.Count > LimitsNum.Value)
                    {
                        throw new DomainException("本次活动人数已满");
                    }
                }
            }

            var participant = new Participant(userId, nickname, avatar, sex, userId == Creator);
            _participants.Add(participant);

            // 加入活动领域事件
            AddDomainEvent(new UserJoinedActivityDomainEvent(participant));
        }

        public void SetFinishedStatus()
        {
            if (ActivityStatusId == ActivityStatus.Processing.Id)
            {
                ActivityStatusId = ActivityStatus.Finished.Id;
            }
        }

        public void SetProcessingStatus()
        {
            if (ActivityStatusId == ActivityStatus.Recruitment.Id)
            {
                ActivityStatusId = ActivityStatus.Processing.Id;
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        public void Obsolete()
        {
            if (ActivityStatusId == ActivityStatus.Recruitment.Id)
            {
                ActivityStatusId = ActivityStatus.Obsoleted.Id;
            }
        }

        /// <summary>
        /// 改变活动状态异常
        /// </summary>
        /// <param name="activityStatusToChange"></param>
        private void StatusChangeException(ActivityStatus activityStatusToChange)
        {
            throw new DomainException($"Is not possible to change the activity status from {ActivityStatus.Name} to {activityStatusToChange.Name}.");
        }
    }
}
