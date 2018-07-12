using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.UserGroup.API.IntegrationEvents
{
    public class AccountCreatedIntegrationEvent
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}
