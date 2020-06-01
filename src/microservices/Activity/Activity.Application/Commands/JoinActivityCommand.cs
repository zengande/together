using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Together.BuildingBlocks.Infrastructure.Identity;

namespace Together.Activity.Application.Commands
{
    [DataContract]
    public class JoinActivityCommand : IRequest<bool>
    {
        [DataMember]
        public UserInfo UserInfo { get; private set; }
        [DataMember]
        public int ActivityId { get; private set; }

        public JoinActivityCommand(int activityId, UserInfo userInfo)
        {
            ActivityId = activityId;
            UserInfo = userInfo;
        }
    }
}
