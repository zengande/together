using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Extensions.Consul
{
    public class ServiceRegisterOptions
    {
        public string ConsulEndpoint { get; set; }
        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string ServiceRegisterUrl { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheckUrl { get; set; }

    }
}
