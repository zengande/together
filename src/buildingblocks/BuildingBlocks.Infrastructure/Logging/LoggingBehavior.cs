using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Together.BuildingBlocks.Infrastructure.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).FullName}");
            _logger.LogDebug($"Handling {typeof(TRequest).FullName} with content {JsonConvert.SerializeObject(request)}");
            var response = await next();
            _logger.LogInformation($"Handled {typeof(TRequest).FullName}");
            return response;
        }
    }
}