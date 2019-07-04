using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Together.Activity.Domain.Events
{
    public class UserJoinedActivityDomainEventHandler
        : INotificationHandler<UserJoinedActivityDomainEvent>
    {
        public Task Handle(UserJoinedActivityDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO : 用户加入了活动
            return Task.CompletedTask;
        }
    }
}
