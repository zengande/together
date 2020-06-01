using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Together.Activity.Application.Commands
{
    [DataContract]
    public class JoinActivityCommand : IRequest<bool>
    {
        [DataMember]
        public string UserId { get; private set; }
        [DataMember]
        public int ActivityId { get; private set; }

        public JoinActivityCommand(int activityId, string userId)
        {
            ActivityId = activityId;
            UserId = userId;
        }
    }
}
