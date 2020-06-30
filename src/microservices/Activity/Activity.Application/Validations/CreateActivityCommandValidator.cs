using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Application.Commands;

namespace Together.Activity.Application.Validations
{
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty();
            RuleFor(a => a.Content)
                .NotEmpty();
        }
    }
}
