using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Together.Identity.API.Configurations
{
    public class ServiceDiscoveryOptions
    {
        public string UserServiceName { get; set; }
        public ConsulDnsEndpointOptions ConsulDnsEndpoint { get; set; }
    }

    public class ConsulDnsEndpointOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public IPEndPoint ToIPEndPoint() => new IPEndPoint(IPAddress.Parse(Address), Port);
    }
}
