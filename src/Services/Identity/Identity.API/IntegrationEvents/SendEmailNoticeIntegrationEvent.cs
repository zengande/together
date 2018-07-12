using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Identity.API.IntegrationEvents
{
    public class SendEmailNoticeIntegrationEvent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }
}
