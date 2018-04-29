using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.Activity.Domain.Exceptions;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class ActivityStatus
        : Enumeration
    {
        public static ActivityStatus Draft = new ActivityStatus(1, nameof(Draft).ToLowerInvariant());
        public static ActivityStatus Normal = new ActivityStatus(2, nameof(Normal).ToLowerInvariant());
        public static ActivityStatus Finished = new ActivityStatus(3, nameof(Finished).ToLowerInvariant());
        public static ActivityStatus TimeOut = new ActivityStatus(4, nameof(TimeOut).ToLowerInvariant());
        public static ActivityStatus Cancelled = new ActivityStatus(5, nameof(Cancelled).ToLowerInvariant());

        protected ActivityStatus() { }

        public ActivityStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ActivityStatus> List() =>
            new[] { Draft, Normal, Finished, TimeOut, Cancelled };

        public static ActivityStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ActivityDomainException($"Possible values for ActivityStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ActivityStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ActivityDomainException($"Possible values for ActivityStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
