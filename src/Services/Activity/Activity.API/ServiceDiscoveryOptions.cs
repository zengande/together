using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Together.Activity.API
{
    public class ServiceDiscoveryOptions
    {
        public string ServiceName { get; set; }
        public string ConsulHttpEndpoint { get; set; }
    }
}
