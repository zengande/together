using System;
using System.Collections.Generic;
using System.Linq;
using Together.Activity.Domain.Exceptions;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class ActivityStatus
        : Enumeration
    {
        /// <summary>
        /// 招募中
        /// </summary>
        public static ActivityStatus Recruitment = new ActivityStatus(1, nameof(Recruitment).ToLowerInvariant());
        /// <summary>
        /// 进行中
        /// </summary>
        public static ActivityStatus Processing = new ActivityStatus(2, nameof(Processing).ToLowerInvariant());
        /// <summary>
        /// 已完结
        /// </summary>
        public static ActivityStatus Finished = new ActivityStatus(3, nameof(Finished).ToLowerInvariant());
        /// <summary>
        /// 暂未定义
        /// </summary>
        public static ActivityStatus TimeOut = new ActivityStatus(4, nameof(TimeOut).ToLowerInvariant());
        /// <summary>
        /// 已作废
        /// </summary>
        public static ActivityStatus Obsoleted = new ActivityStatus(5, nameof(Obsoleted).ToLowerInvariant());

        protected ActivityStatus() { }

        public ActivityStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ActivityStatus> List() =>
            new[] { Recruitment, Processing, Finished, TimeOut, Obsoleted };

        public static ActivityStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ActivityDomainException($"Possible values for ActivityStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ActivityStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ActivityDomainException($"Possible values for ActivityStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
