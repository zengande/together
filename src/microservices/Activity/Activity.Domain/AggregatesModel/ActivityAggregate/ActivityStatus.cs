using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class ActivityStatus : Enumeration
    {
        /// <summary>
        /// 招募中
        /// </summary>
        public static ActivityStatus Recruitment = new ActivityStatus(1, nameof(Recruitment).ToUpperInvariant());
        /// <summary>
        /// 进行中
        /// </summary>
        public static ActivityStatus Processing = new ActivityStatus(2, nameof(Processing).ToUpperInvariant());
        /// <summary>
        /// 已完结
        /// </summary>
        public static ActivityStatus Finished = new ActivityStatus(3, nameof(Finished).ToUpperInvariant());
        /// <summary>
        /// 超时
        /// </summary>
        public static ActivityStatus TimeOut = new ActivityStatus(4, nameof(TimeOut).ToUpperInvariant());
        /// <summary>
        /// 已作废
        /// </summary>
        public static ActivityStatus Obsoleted = new ActivityStatus(5, nameof(Obsoleted).ToUpperInvariant());

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
                throw new DomainException($"Possible values for ActivityStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ActivityStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new DomainException($"Possible values for ActivityStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
