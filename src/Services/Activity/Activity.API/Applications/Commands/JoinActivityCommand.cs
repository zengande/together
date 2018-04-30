using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Models;

namespace Together.Activity.API.Applications.Commands
{
    public class JoinActivityCommand
        : IRequest<bool>
    {
        public CurrentUser User { get; private set; }
        public int ActivityId { get; private set; }
        public JoinActivityCommand(int activityId, CurrentUser user)
        {
            ActivityId = activityId;
            User = user;
        }
    }
}
