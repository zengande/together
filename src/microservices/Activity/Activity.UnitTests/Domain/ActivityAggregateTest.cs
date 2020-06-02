using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Xunit;

namespace Activity.UnitTests.Domain
{
    public class ActivityAggregateTest
    {
        [Fact]
        public void Add_new_Activity_raises_new_event()
        {
            var creator = new Participant("1", "fake user", "avatar", 1, true);
            var address = new Address("provice", "city", "detail address", 0, 0);
            var title = "fake activity";
            var content = "content";
            var endRegisterTime = DateTime.Now.AddDays(1);
            var startTime = DateTime.Now.AddDays(2);
            var endTime = DateTime.Now.AddDays(2).AddHours(4);
            var expectedResult = 1;

            var activity = new Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity(creator, title, content, endRegisterTime, startTime, endTime, address, 0, AddressVisibleRule.PublicVisible);

            Assert.Equal(expectedResult, activity.DomainEvents.Count);
        }
    }
}
