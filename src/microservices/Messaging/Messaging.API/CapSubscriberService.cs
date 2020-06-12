using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Messaging.Application.IntegrationEvents;

namespace Together.Messaging.API
{
    public class CapSubscriberService : ICapSubscribe
    {
        private readonly ILogger<CapSubscriberService> _logger;
        public CapSubscriberService(ILogger<CapSubscriberService> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("Together.Messaging.Realtime.NewUserJoined")]
        public void SendNewUserJoinedNotice(NewUserJoinedActivityIntegrationEvent @event)
        {
            _logger.LogInformation($"用户【{@event.UserName}】加入活动【{@event.Title}】！");
        }
    }
}
