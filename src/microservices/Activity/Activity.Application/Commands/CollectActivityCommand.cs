using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.Commands
{
    public class CollectActivityCommand : IRequest<bool>
    {
        public string UserId { get; private set; }
        public int ActivityId { get; private set; }

        public CollectActivityCommand(int activityId, string userId)
        {
            ActivityId = activityId;
            UserId = userId;
        }
    }
}
