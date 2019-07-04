using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Together.Notice.Hubs;
using Together.Notice.IntegrationEvents;

namespace Together.Notice.IntegrationEventHandlers
{
    public class NewUserJoinedActivityEventHandler
        : ICapSubscribe
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        public NewUserJoinedActivityEventHandler(IHubContext<NotificationsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [CapSubscribe("Together.Notice.Realtime.NewUserJoined")]
        public async Task NotifiyHasNewUserJoined(NewUserJoinedActivityIntegrationEvent @event)
        {
            await _hubContext.Clients
                .Group(@event.OwnerId)
                .SendAsync("NewUserJoined", new { title = @event.ActivityTitle, @event.Who, @event.JoinTime });
        }
    }
}
