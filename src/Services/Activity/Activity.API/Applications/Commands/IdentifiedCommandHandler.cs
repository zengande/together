using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.Infrastructure.Idempotency;

namespace Together.Activity.API.Applications.Commands
{
    public class IdentifiedCommandHandler<TCommand, TResult> : IRequestHandler<IdentifiedCommand<TCommand, TResult>, TResult>
        where TCommand : IRequest<TResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IdentifiedCommandHandler<TCommand, TResult>> _logger;
        private readonly IRequestManager _requestManager;
        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<TCommand, TResult>> logger)
        {
            _mediator = mediator;
            _requestManager = requestManager;
            _logger = logger;
        }

        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual TResult CreateResultForDuplicateRequest()
        {
            return default(TResult);
        }

        public async Task<TResult> Handle(IdentifiedCommand<TCommand, TResult> message, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(message.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await _requestManager.CreateRequestForCommandAsync<TCommand>(message.Id);
                try
                {
                    // Send the embeded business command to mediator so it runs its related CommandHandler 
                    var result = await _mediator.Send(message.Command);
                    return result;
                }
                catch (Exception e)
                {
                    _logger.LogError($"{e.Message} => {e.InnerException}");
                    return CreateResultForDuplicateRequest();
                }
            }
        }
    }
}
