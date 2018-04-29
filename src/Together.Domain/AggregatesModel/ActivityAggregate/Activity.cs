using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.Domain.SeedWork;

namespace Together.Domain.AggregatesModel.ActivityAggregate
{
    public class Activity
        : Entity, IAggregateRoot
    {
        /// <summary>
        /// 活动发起人
        /// </summary>
        private int? _ownerId;

        public ActivityStatus ActivityStatus { get; private set; }
        private int _activityStatusId;

        /// <summary>
        /// 活动描述
        /// </summary>
        private string _discription;

        /// <summary>
        /// 活动详情
        /// </summary>
        private string _details;

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _createTime;

        /// <summary>
        /// 截止报名时间
        /// </summary>
        private DateTime _endTime;

        /// <summary>
        /// 活动时间
        /// </summary>
        private DateTime _activityTime;

        /// <summary>
        /// 活动地点
        /// </summary>
        private string _address;

        /// <summary>
        /// 限制人数
        /// </summary>
        private int? _limitsNum;

        /// <summary>
        /// 参与者
        /// </summary>
        private readonly List<User> _participant;
        public IReadOnlyCollection<User> Participant => _participant;

        protected Activity()
        {
            _participant = new List<User>();
            _createTime = DateTime.Now;
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
        public Activity(int userId, string discription, string details, DateTime endTime, DateTime activityTime, string address, int? limitsNum)
        {
            _ownerId = userId;
            _discription = discription;
            _details = details;
            _endTime = endTime;
            _activityTime = activityTime;
            _address = address;
            _limitsNum = limitsNum;

            // CreateActivityDomainEvent(userId, discription,details,endTime,activityTime,address,limitsNum);
        }

        /// <summary>
        /// 提交活动
        /// </summary>
        public void SubmitActivity()
        {
            
        }

        /// <summary>
        /// 加入活动
        /// </summary>
        public void JoinActivity(int userId)
        {
            // 已参加
            if(_participant.Any(u=>u.Id == userId))
            {
                return;
            }

            var user = new User();
            _participant.Add(user);
        }

        public IEnumerable<User> GetAllParticipant() => _participant;


    }
}
