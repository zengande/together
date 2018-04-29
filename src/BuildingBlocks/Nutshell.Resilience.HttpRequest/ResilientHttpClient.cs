using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nutshell.Resilience.HttpRequest.abstracts;
using Polly;
using Polly.Wrap;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Resilience.HttpRequest
{
    public class ResilientHttpClient
        : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<ResilientHttpClient> _logger;
        private readonly Func<string, IEnumerable<Policy>> _policyCreator;
        private ConcurrentDictionary<string, PolicyWrap> _policyWrappers;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResilientHttpClient(Func<string, IEnumerable<Policy>> policyCreator,
            ILogger<ResilientHttpClient> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _logger = logger;
            _policyCreator = policyCreator;
            _policyWrappers = new ConcurrentDictionary<string, PolicyWrap>();
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item, authorizationToken, requestId, authorizationMethod);
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
        private Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string authorizationToken, string requestId, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post &&
                method != HttpMethod.Put)
            {
                throw new ArgumentException($"请求方式有误，必须为 Post/Put，当前方式为{method}");
            }
            var origin = GetOriginFromUri(uri);

            return HttpInvoker(origin, async () =>
            {
                var requestMessage = new HttpRequestMessage(method, uri);

                SetAuthorizationHeader(requestMessage);

                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                if (requestId != null)
                {
                    requestMessage.Headers.Add("x-requestid", requestId);
                }

                var response = await _client.SendAsync(requestMessage);

                // raise exception if HttpResponseCode 500 
                // needed for circuit breaker to track fails

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }

        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }

        private async Task<T> HttpInvoker<T>(string origin, Func<Task<T>> task)
        {
            var normalizedOrigin = NormalizeOrigin(origin);
            if(!_policyWrappers.TryGetValue(normalizedOrigin, out var wrap))
            {
                wrap = Policy.WrapAsync(_policyCreator(normalizedOrigin).ToArray());
                _policyWrappers.TryAdd(normalizedOrigin, wrap);
            }
            #pragma warning disable CS0618 // 类型或成员已过时
            return await wrap.ExecuteAsync(task, new Context(normalizedOrigin));
            #pragma warning restore CS0618 // 类型或成员已过时
        }

        private string NormalizeOrigin(string origin)
        {
            return origin?.Trim()?.ToLower();
        }

        private string GetOriginFromUri(string uri)
        {
            var url = new Uri(uri);
            var origin = $"{url.Scheme}://{url.DnsSafeHost}:{url.Port}";
            return origin;
        }
    }
}
