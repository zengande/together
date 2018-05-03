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
        public ConsulOptions Consul { get; set; }
    }

    public class ConsulOptions
    {
        public string HttpEndpoint { get; set; }
        public DnsEndpointOptions DnsEndpoint { get; set; }
    }

    public class DnsEndpointOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public IPEndPoint ToIPEndPoint() => new IPEndPoint(IPAddress.Parse(Address), Port);
    }
}
