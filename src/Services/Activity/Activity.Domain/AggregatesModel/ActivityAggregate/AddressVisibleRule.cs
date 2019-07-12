using System.Collections.Generic;
using System.Linq;
using Together.Activity.Domain.Exceptions;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class AddressVisibleRule
        : Enumeration
    {
        /// <summary>
        /// 公开
        /// </summary>
        public static AddressVisibleRule PublicVisible = new AddressVisibleRule(1, nameof(PublicVisible).ToLowerInvariant());
        /// <summary>
        /// 参与者可见
        /// </summary>
        public static AddressVisibleRule ParticipantsVisible = new AddressVisibleRule(2, nameof(ParticipantsVisible).ToLowerInvariant());

        protected AddressVisibleRule() { }

        public AddressVisibleRule(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<AddressVisibleRule> List() =>
            new[] { PublicVisible, ParticipantsVisible };

        public static AddressVisibleRule From(int id)
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
