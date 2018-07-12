using Microsoft.AspNetCore.SignalR;
using Nutshell.Common.Cache;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Hubs
{
    public class NoticeHub
        : Hub
    {
        private readonly ICacheService _cacheService;
        public NoticeHub(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public override async Task OnConnectedAsync()
        {
            var subjectId = GetSubjectId();
            if (!string.IsNullOrEmpty(subjectId))
            {
                var connectionId = Context.ConnectionId;
                await _cacheService.ReplaceAsync(subjectId, connectionId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var subjectId = GetSubjectId();
            if (!string.IsNullOrEmpty(subjectId))
            {
                if(await _cacheService.ExistsAsync(subjectId))
                {
                    await _cacheService.RemoveAsync(subjectId);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        private string GetSubjectId()
        {
            return Context.User?.Claims?.First(c => c.Type == "sub")?.Value;
        }

        public async Task Notice()
        {
            var user = (await _cacheService.GetAsync<string>("d28f5687-c3e6-4385-ad61-ebb2b8e7a586"))?.ToString();
            if (!string.IsNullOrEmpty(user))
            {
                if (Context.ConnectionId != user)
                {
                    await Clients.Client(user).SendAsync("Notice", $"hi,{user}. 来人了~~");
                }
            }
        }
    }
}
