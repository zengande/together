using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Application.Commands;

namespace Together.Activity.Application.Validations
{
    public class JoinActivityCommandValidator : AbstractValidator<JoinActivityCommand>
    {
        public JoinActivityCommandValidator()
        {
            RuleFor(r => r.UserInfo)
                .NotNull();
            RuleFor(r => r.ActivityId)
                .GreaterThan(0);
        }
    }
}
