using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure.HttpClientHandlers
{
    public class HttpClientRequestIdDelegatingHandler
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
            {
                request.Headers.Add("x-requestid", Guid.NewGuid().ToString());
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
