using MediatR;
using System;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;

namespace Together.Activity.Application.Commands
{
    public class CreateActivityCommand : IRequest<int>
    {
        public Participant Creator { get; private set; }
        public Address Address { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime EndRegisterTime { get; private set; }
        public DateTime ActivityStartTime { get; private set; }
        public DateTime ActivityEndTime { get; private set; }
        public int? LimitsNum { get; private set; }
        public int AddressVisibleRuleId { get; private set; }
        public CreateActivityCommand(Participant creator, string title, string content, DateTime endRegisterTime, DateTime activityStartTime, DateTime activityEndTime, Address address, int addressVisibleRuleId, int? limitsNum)
        {
            Creator = creator;
            Title = title;
            Content = content;
            EndRegisterTime = endRegisterTime;
            ActivityStartTime = activityStartTime;
            ActivityEndTime = activityEndTime;
            Address = address;
            AddressVisibleRuleId = addressVisibleRuleId;
            LimitsNum = limitsNum;
        }
    }
}
