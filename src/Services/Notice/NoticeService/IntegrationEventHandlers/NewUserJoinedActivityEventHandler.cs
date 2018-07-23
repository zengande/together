using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task NotifiyHasNewUserJoined()
        {
            await _hubContext.Clients
                .Group("zengande")
                .SendAsync("NewUserJoined", "测试活动", "张三");
        }
    }
}
