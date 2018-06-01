using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.IntegrationEvents
{
    public class VerifyAccountEmailNoticeEvent
    {
        public string To { get; set; }
        public string Link { get; set; }
    }
}
