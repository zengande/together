using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Together.Extensions.Claims;

namespace Together.Notice.Hubs
{
    [Authorize]
    public class NotificationsHub
        : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (string.IsNullOrEmpty(CurrentUser?.UserId) == false)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, CurrentUser?.UserId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (string.IsNullOrEmpty(CurrentUser?.UserId) == false)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, CurrentUser?.UserId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private CurrentUserInfo CurrentUser => Context.GetHttpContext().User.GetCurrentUserInfo();
    }
}
