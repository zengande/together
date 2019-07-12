using MediatR;
using System;

namespace Together.Activity.API.Applications.Commands
{
    public class IdentifiedCommand<TCommand, TResult>
        : IRequest<TResult> where TCommand : IRequest<TResult>
    {
        public TCommand Command { get; }
        public Guid Id { get; set; }

        public IdentifiedCommand(TCommand command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
