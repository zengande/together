using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Services;
using Together.UserGroup.API.IntegrationEvents;

namespace Together.UserGroup.API.IntegrationEventHandlers
{
    public class AccountCreatedIntegrationEventHandler
        : ICapSubscribe
    {
        private readonly IUserService _userService;
        public AccountCreatedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        [CapSubscribe("Identity.API.AccountCreatedEvent")]
        public async Task CreateUser(AccountCreatedIntegrationEvent @event)
        {
            if (!_userService.Existed(u => u.Id.Equals(@event.Id, StringComparison.CurrentCultureIgnoreCase)))
            {
                await _userService.AddAsync(new Infrastructure.Models.User
                {
                    Id = @event.Id,
                    Nickname = @event.Nickname,
                    Email = @event.Email
                });
                await _userService.SaveChangesAsync();
            }
        }
    }
}
