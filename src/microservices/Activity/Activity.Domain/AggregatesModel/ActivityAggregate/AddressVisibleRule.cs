using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class AddressVisibleRule : Enumeration
    {
        /// <summary>
        /// 公开
        /// </summary>
        public static AddressVisibleRule PublicVisible = new AddressVisibleRule(1, nameof(PublicVisible).ToUpperInvariant());
        /// <summary>
        /// 参与者可见
        /// </summary>
        public static AddressVisibleRule ParticipantsVisible = new AddressVisibleRule(2, nameof(ParticipantsVisible).ToUpperInvariant());

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
                throw new DomainException($"Possible values for ActivityStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
