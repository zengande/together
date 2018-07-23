using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.IntegrationEvents.Events
{
    public class ActivityExpiredIntegrationEvent
    {
        public int ActivityId { get; set; }
    }
}
