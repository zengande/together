using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Application.Commands;

namespace Together.Activity.Application.Validations
{
    public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateActivityCommand, int>>
    {
        public IdentifiedCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
