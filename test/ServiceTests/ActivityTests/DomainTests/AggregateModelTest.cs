using System;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.Exceptions;
using Xunit;

namespace DomainTests
{
    public class AggregateModelTest
    {
        [Fact]
        public void Create_activit_and_join_activity()
        {
            var userId = "1564646";
            var desc = "这是一个测试的活动";
            var details = "详细信息";
            var activity = new Activity(userId, desc, details, DateTime.Now.AddDays(7), DateTime.Now.AddDays(10), DateTime.Now.AddDays(10).AddHours(4), new Address("Beijing", "Beijing", "", 32d, 120d), 0, AddressVisibleRule.PublicVisible, 1);

            Assert.Equal(0, activity.Participants.Count);

            var joinUser = "788745";
            activity.JoinActivity(joinUser, "", "", 1);

            Assert.Equal(1, activity.Participants.Count);

            activity.JoinActivity(joinUser, "", "", 1);
            Assert.Equal(1, activity.Participants.Count);

            var expected = typeof(ActivityDomainException);
            Type actual = null;
            try
            {
                var other_joinUser = "9456456";
                activity.JoinActivity(other_joinUser, "", "", 1);
            }
            catch (Exception e)
            {
                actual = e.GetType();
            }
            Assert.Equal(expected, actual);
        }
    }
}
