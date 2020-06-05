using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Together.BuildingBlocks.Infrastructure.Idempotency;

namespace Together.Activity.Application.Commands
{
    public class IdentifiedCommandHandler<TRequest, TResponse> : IRequestHandler<IdentifiedCommand<TRequest, TResponse>, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        public IdentifiedCommandHandler(IMediator mediator,
            IRequestManager requestManager)
        {
            _mediator = mediator;
            _requestManager = requestManager;
        }

        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual TResponse CreateResultForDuplicateRequest()
        {
            return default;
        }

        public async Task<TResponse> Handle(IdentifiedCommand<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(request.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await _requestManager.CreateRequestForCommandAsync<TRequest>(request.Id);

                // Send the embeded business command to mediator so it runs its related CommandHandler 
                var result = await _mediator.Send(request.Command);
                return result;
            }
        }
    }
}
