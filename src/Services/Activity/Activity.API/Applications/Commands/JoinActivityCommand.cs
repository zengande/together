using MediatR;
using Together.Activity.API.Models;
using Together.Extensions.Claims;

namespace Together.Activity.API.Applications.Commands
{
    public class JoinActivityCommand
        : IRequest<bool>
    {
        public CurrentUserInfo User { get; private set; }
        public int ActivityId { get; private set; }
        public JoinActivityCommand(int activityId, CurrentUserInfo user)
        {
            ActivityId = activityId;
            User = user;
        }
    }
}
