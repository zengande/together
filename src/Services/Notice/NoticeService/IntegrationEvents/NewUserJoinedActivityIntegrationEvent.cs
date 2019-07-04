using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.IntegrationEvents
{
    public class NewUserJoinedActivityIntegrationEvent
    {
        public string OwnerId { get; set; }
        public string ActivityTitle { get; set; }
        public string Who { get; set; }
        public DateTime JoinTime { get; set; }
    }
}
