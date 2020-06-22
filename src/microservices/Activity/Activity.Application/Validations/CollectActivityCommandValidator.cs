using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Application.Commands;

namespace Together.Activity.Application.Validations
{
    public class CollectActivityCommandValidator: AbstractValidator<CollectActivityCommand>
    {
        public CollectActivityCommandValidator()
        {
            RuleFor(c => c.ActivityId)
                .GreaterThan(0);
            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}
